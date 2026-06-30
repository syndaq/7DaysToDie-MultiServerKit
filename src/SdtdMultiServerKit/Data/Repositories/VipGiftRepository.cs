using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Goodsrepository
    /// </summary>
    public class VipGiftRepository : DefaultRepository<T_VipGift>, IVipGiftRepository
    {
        /// <inheritdoc/>
        public Task<int> ResetClaimStateAsync()
        {
            string sql = $"UPDATE {SqlGenerator.TableName} SET ClaimState=0";
            return base.ExecuteAsync(sql, useTransaction: true);
        }
    }
}