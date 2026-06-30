using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILotteryPoolItemRepository : IRepository<T_LotteryPoolItem>
    {
        Task<int> DeleteByLotteryPoolIdAsync(int lotteryPoolId);
    }
}
