using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Gift repository
    /// </summary>
    public interface IVipGiftRepository : IRepository<T_VipGift>
    {
        /// <summary>
        /// Reset claim state
        /// </summary>
        /// <returns></returns>
        Task<int> ResetClaimStateAsync();
    }
}