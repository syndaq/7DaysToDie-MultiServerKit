using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// 
    /// </summary>
    public interface IVipGiftCommandRepository : IRepository<T_VipGiftCommand>
    {
        /// <summary>
        /// IdDeleterecord
        /// </summary>
        /// <param name="vipGiftId"></param>
        /// <returns></returns>
        Task<int> DeleteByVipGiftIdAsync(string vipGiftId);

    }
}