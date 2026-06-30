using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IGoodsCommandRepository : IRepository<T_GoodsCommand>
    {
        /// <summary>
        /// GoodsIdDeleterecord
        /// </summary>
        /// <param name="goodsId"></param>
        /// <returns></returns>
        Task<int> DeleteByGoodsIdAsync(int goodsId);

    }
}