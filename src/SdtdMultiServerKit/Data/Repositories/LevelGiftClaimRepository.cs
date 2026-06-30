using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LevelGiftClaimRepository : DefaultRepository<T_LevelGiftClaim>, ILevelGiftClaimRepository
    {
        public async Task<bool> HasClaimedAsync(string playerId, string levelGiftId)
        {
            var claim = await base.GetFirstOrDefaultAsync(
                "PlayerId=@PlayerId AND LevelGiftId=@LevelGiftId",
                param: new { PlayerId = playerId, LevelGiftId = levelGiftId });
            return claim != null;
        }

        public Task<int> RecordClaimAsync(string playerId, string levelGiftId)
        {
            return base.InsertAsync(new T_LevelGiftClaim
            {
                PlayerId = playerId,
                LevelGiftId = levelGiftId,
                ClaimedAt = DateTime.Now,
            });
        }
    }
}
