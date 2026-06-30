using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class VipGiftItemRepository : DefaultRepository<T_VipGiftItem>, IVipGiftItemRepository
    {
        /// <inheritdoc/>
        public Task<int> DeleteByVipGiftIdAsync(string vipGiftId)
        {
            return base.DeleteAsync("VipGiftId=@VipGiftId", param: new { VipGiftId = vipGiftId });
        }
    }
}