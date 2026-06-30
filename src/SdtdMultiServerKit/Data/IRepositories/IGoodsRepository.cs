using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Goodsrepository
    /// </summary>
    public interface IGoodsRepository : IRepository<T_Goods>
    {
        /// <summary>
        /// GetallGoodsIdSort
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<T_Goods>> GetAllOrderByIdAsync();
    }
}