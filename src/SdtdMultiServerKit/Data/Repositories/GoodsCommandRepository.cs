using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class GoodsCommandRepository : DefaultRepository<T_GoodsCommand>, IGoodsCommandRepository
    {
        /// <inheritdoc/>
        public Task<int> DeleteByGoodsIdAsync(int goodsId)
        {
            return base.DeleteAsync("GoodsId=@GoodsId", param: new { GoodsId = goodsId });
        }
    }
}