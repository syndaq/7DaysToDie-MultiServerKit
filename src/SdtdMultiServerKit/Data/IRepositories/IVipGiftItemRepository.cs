using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVipGiftItemRepository : IRepository<T_VipGiftItem>
    {
        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <param name="vipGiftId"></param>
        /// <returns></returns>
        Task<int> DeleteByVipGiftIdAsync(string vipGiftId);
    }
}