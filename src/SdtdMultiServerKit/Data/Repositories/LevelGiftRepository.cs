using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LevelGiftRepository : DefaultRepository<T_LevelGift>, ILevelGiftRepository
    {
        public Task<int> ResetClaimStateAsync()
        {
            string sql = $"UPDATE {SqlGenerator.TableName} SET ClaimState=0";
            return base.ExecuteAsync(sql, useTransaction: true);
        }

        public Task<T_LevelGift?> GetByPlayerIdAsync(string playerId)
        {
            return base.GetFirstOrDefaultAsync("Id=@PlayerId", param: new { PlayerId = playerId });
        }

        public Task<IEnumerable<T_LevelGift>> GetEligibleLevelGiftsAsync(int playerLevel)
        {
            return base.GetListAsync("RequiredLevel<=@PlayerLevel ORDER BY RequiredLevel DESC", param: new { PlayerLevel = playerLevel });
        }
    }
}
