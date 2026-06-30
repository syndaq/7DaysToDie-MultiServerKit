using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILotteryPoolRepository : IRepository<T_LotteryPool>
    {
        Task<IEnumerable<T_LotteryPool>> GetAllOrderByIdAsync();

        Task<IEnumerable<T_LotteryPool>> GetEnabledPoolsAsync();
    }
}
