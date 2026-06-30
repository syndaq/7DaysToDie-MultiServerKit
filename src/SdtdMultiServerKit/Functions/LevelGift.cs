using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Variables;

namespace SdtdMultiServerKit.Functions
{
    public class LevelGift : FunctionBase<LevelGiftSettings>
    {
        private readonly ILevelGiftRepository _levelGiftRepository;
        private readonly ILevelGiftClaimRepository _levelGiftClaimRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly ICommandListRepository _commandListRepository;

        public LevelGift(
            ILevelGiftRepository levelGiftRepository,
            ILevelGiftClaimRepository levelGiftClaimRepository,
            IItemListRepository itemListRepository,
            ICommandListRepository commandListRepository)
        {
            _levelGiftRepository = levelGiftRepository;
            _levelGiftClaimRepository = levelGiftClaimRepository;
            _itemListRepository = itemListRepository;
            _commandListRepository = commandListRepository;
        }

        protected override async Task<bool> OnChatCmd(string message, ManagedPlayer managedPlayer)
        {
            if (!string.Equals(message, Settings.ClaimCmd, StringComparison.OrdinalIgnoreCase))
            {
                return false;
            }

            string playerId = managedPlayer.PlayerId;
            int playerLevel = managedPlayer.EntityPlayer.Progression.Level;

            var playerGift = await _levelGiftRepository.GetByPlayerIdAsync(playerId);
            if (playerGift != null)
            {
                if (playerGift.ClaimState)
                {
                    SendMessageToPlayer(playerId, FormatCmd(Settings.HasClaimedTip, managedPlayer, playerGift));
                    return true;
                }

                if (playerLevel < playerGift.RequiredLevel)
                {
                    SendMessageToPlayer(playerId, FormatCmd(Settings.LevelNotEnoughTip, managedPlayer, playerGift));
                    return true;
                }

                await GrantGiftAsync(managedPlayer, playerGift, isPlayerBound: true);
                return true;
            }

            var eligibleGifts = (await _levelGiftRepository.GetEligibleLevelGiftsAsync(playerLevel)).ToList();
            foreach (var gift in eligibleGifts)
            {
                if (IsPlayerBoundGift(gift, playerId))
                {
                    continue;
                }

                if (await _levelGiftClaimRepository.HasClaimedAsync(playerId, gift.Id))
                {
                    continue;
                }

                await GrantGiftAsync(managedPlayer, gift, isPlayerBound: false);
                return true;
            }

            var allGifts = (await _levelGiftRepository.GetAllAsync()).OrderBy(gift => gift.RequiredLevel).ToList();
            T_LevelGift? nextGift = null;
            foreach (var gift in allGifts)
            {
                if (IsPlayerBoundGift(gift, playerId) || gift.RequiredLevel <= playerLevel)
                {
                    continue;
                }

                if (await _levelGiftClaimRepository.HasClaimedAsync(playerId, gift.Id))
                {
                    continue;
                }

                nextGift = gift;
                break;
            }

            if (nextGift != null)
            {
                SendMessageToPlayer(playerId, FormatCmd(Settings.LevelNotEnoughTip, managedPlayer, nextGift));
                return true;
            }

            SendMessageToPlayer(playerId, Settings.NoGiftTip);
            return true;
        }

        private async Task GrantGiftAsync(ManagedPlayer managedPlayer, T_LevelGift gift, bool isPlayerBound)
        {
            string playerId = managedPlayer.PlayerId;

            var itemList = await _itemListRepository.GetListByLevelGiftIdAsync(gift.Id);
            foreach (var item in itemList)
            {
                Utilities.Utils.GiveItem(playerId, item.ItemName, item.Count, item.Quality, item.Durability);
                await Task.Delay(20);
            }

            var commandList = await _commandListRepository.GetListByLevelGiftIdAsync(gift.Id);
            foreach (var item in commandList)
            {
                foreach (var cmd in item.Command.Split('\n'))
                {
                    Utilities.Utils.ExecuteConsoleCommand(FormatCmd(cmd, managedPlayer, gift), item.InMainThread);
                }
                await Task.Delay(20);
            }

            gift.TotalClaimCount++;
            gift.LastClaimAt = DateTime.Now;

            if (isPlayerBound)
            {
                gift.ClaimState = true;
                await _levelGiftRepository.UpdateAsync(gift);
            }
            else
            {
                await _levelGiftClaimRepository.RecordClaimAsync(playerId, gift.Id);
                await _levelGiftRepository.UpdateAsync(gift);
            }

            SendMessageToPlayer(playerId, FormatCmd(Settings.ClaimSuccessTip, managedPlayer, gift));
            CustomLogger.Info(
                "PlayerId: {0}, playerName: {1}, claimed level gift: {2} (level {3}).",
                playerId,
                managedPlayer.PlayerName,
                gift.Name,
                gift.RequiredLevel);
        }

        private static bool IsPlayerBoundGift(T_LevelGift gift, string playerId)
        {
            return string.Equals(gift.Id, playerId, StringComparison.OrdinalIgnoreCase);
        }

        private static string FormatCmd(string message, ManagedPlayer player, T_LevelGift gift)
        {
            return StringTemplate.Render(message, new LevelGiftVariables
            {
                EntityId = player.EntityId,
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                GiftName = gift.Name,
                RequiredLevel = gift.RequiredLevel,
                TotalClaimCount = gift.TotalClaimCount,
                GiftDescription = gift.Description,
            });
        }
    }
}
