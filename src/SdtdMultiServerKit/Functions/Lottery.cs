using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;
using SdtdMultiServerKit.FunctionSettings;
using SdtdMultiServerKit.Managers;
using SdtdMultiServerKit.Variables;

namespace SdtdMultiServerKit.Functions
{
    public class Lottery : FunctionBase<LotterySettings>
    {
        private readonly ILotteryPoolRepository _lotteryPoolRepository;
        private readonly IPointsInfoRepository _pointsInfoRepository;
        private readonly IItemListRepository _itemListRepository;
        private readonly ICommandListRepository _commandListRepository;
        private readonly Dictionary<string, DateTime> _lastDrawAt = new();

        public Lottery(
            ILotteryPoolRepository lotteryPoolRepository,
            IPointsInfoRepository pointsInfoRepository,
            IItemListRepository itemListRepository,
            ICommandListRepository commandListRepository)
        {
            _lotteryPoolRepository = lotteryPoolRepository;
            _pointsInfoRepository = pointsInfoRepository;
            _itemListRepository = itemListRepository;
            _commandListRepository = commandListRepository;
        }

        protected override async Task<bool> OnChatCmd(string message, ManagedPlayer managedPlayer)
        {
            if (string.Equals(message, Settings.QueryListCmd, StringComparison.OrdinalIgnoreCase))
            {
                string playerId = managedPlayer.PlayerId;
                var pools = await _lotteryPoolRepository.GetEnabledPoolsAsync();
                if (pools.Any() == false)
                {
                    SendMessageToPlayer(playerId, Settings.NoPoolTip);
                }
                else
                {
                    foreach (var pool in pools)
                    {
                        SendMessageToPlayer(playerId, FormatCmd(Settings.PoolItemTip, managedPlayer, pool));
                    }
                }

                return true;
            }

            if (message.StartsWith(Settings.DrawCmdPrefix + ConfigManager.GlobalSettings.ChatCommandSeparator, StringComparison.OrdinalIgnoreCase))
            {
                int poolId = message.Substring(Settings.DrawCmdPrefix.Length + ConfigManager.GlobalSettings.ChatCommandSeparator.Length).ToInt(-1);
                if (poolId < 0)
                {
                    return false;
                }

                return await DrawPoolAsync(managedPlayer, poolId);
            }

            return false;
        }

        private async Task<bool> DrawPoolAsync(ManagedPlayer managedPlayer, int poolId)
        {
            string playerId = managedPlayer.PlayerId;
            var pool = await _lotteryPoolRepository.GetByIdAsync(poolId);
            if (pool == null || pool.IsEnabled == false)
            {
                return false;
            }

            if (Settings.DrawInterval > 0
                && _lastDrawAt.TryGetValue(playerId, out var lastDrawAt)
                && DateTime.Now < lastDrawAt.AddSeconds(Settings.DrawInterval))
            {
                SendMessageToPlayer(playerId, Settings.CoolingTip);
                return true;
            }

            int drawCost = pool.DrawCost > 0 ? pool.DrawCost : Settings.DrawCost;
            if (drawCost <= 0)
            {
                drawCost = 0;
            }

            if (drawCost > 0)
            {
                int points = await _pointsInfoRepository.GetPointsByIdAsync(playerId);
                if (points < drawCost)
                {
                    SendMessageToPlayer(playerId, FormatCmd(Settings.PointsNotEnoughTip, managedPlayer, pool));
                    return true;
                }

                await _pointsInfoRepository.ChangePointsAsync(playerId, -drawCost);
            }

            var itemList = await _itemListRepository.GetListByLotteryPoolIdAsync(pool.Id);
            foreach (var item in itemList)
            {
                Utilities.Utils.GiveItem(playerId, item.ItemName, item.Count, item.Quality, item.Durability);
                await Task.Delay(20);
            }

            var commandList = await _commandListRepository.GetListByLotteryPoolIdAsync(pool.Id);
            foreach (var item in commandList)
            {
                foreach (var cmd in item.Command.Split('\n'))
                {
                    Utilities.Utils.ExecuteConsoleCommand(FormatCmd(cmd, managedPlayer, pool), item.InMainThread);
                }
                await Task.Delay(20);
            }

            _lastDrawAt[playerId] = DateTime.Now;
            SendMessageToPlayer(playerId, FormatCmd(Settings.DrawSuccessTip, managedPlayer, pool));
            CustomLogger.Info("PlayerId: {0}, playerName: {1}, drew lottery pool: {2}.", playerId, managedPlayer.PlayerName, pool.Name);
            return true;
        }

        private static string FormatCmd(string message, ManagedPlayer player, T_LotteryPool pool)
        {
            return StringTemplate.Render(message, new LotteryVariables()
            {
                PlayerId = player.PlayerId,
                PlayerName = player.PlayerName,
                EntityId = player.EntityId,
                PoolId = pool.Id,
                PoolName = pool.Name,
                DrawCost = pool.DrawCost,
                Weight = pool.Weight,
            });
        }
    }
}
