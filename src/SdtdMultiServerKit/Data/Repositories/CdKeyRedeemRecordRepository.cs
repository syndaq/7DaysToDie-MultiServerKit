using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Represents a Cd Key Redeem Record repository.
    /// </summary>
    public class CdKeyRedeemRecordRepository : DefaultRepository<CdKeyRedeemRecord>, ICdKeyRedeemRecordRepository
    {
        public Task<CdKeyRedeemRecord?> GetByKeyAndPlayerIdAsync(string key, string playerId)
        {
            return base.GetFirstOrDefaultAsync("[Key]=@Key AND [PlayerId]=@PlayerId", param: new { Key = key, PlayerId = playerId });
        }
    }
}