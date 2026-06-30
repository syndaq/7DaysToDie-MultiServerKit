using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILotteryPoolCommandRepository : IRepository<T_LotteryPoolCommand>
    {
        Task<int> DeleteByLotteryPoolIdAsync(int lotteryPoolId);
    }
}
