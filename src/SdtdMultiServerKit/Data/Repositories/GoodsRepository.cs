using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Goodsrepository
    /// </summary>
    public class GoodsRepository : DefaultRepository<T_Goods>, IGoodsRepository
    {
        /// <inheritdoc/>
        public Task<IEnumerable<T_Goods>> GetAllOrderByIdAsync()
        {
            return base.GetListAsync(orderByClause: "Id ASC");
        }
    }
}