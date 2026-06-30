using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsItemRepository : DefaultRepository<T_GoodsItem>, IGoodsItemRepository
    {
        /// <inheritdoc/>
        public Task<int> DeleteByGoodsIdAsync(int goodsId)
        {
            return base.DeleteAsync("GoodsId=@GoodsId", param: new { GoodsId = goodsId });
        }
    }
}