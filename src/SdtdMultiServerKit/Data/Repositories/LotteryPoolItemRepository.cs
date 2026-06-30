using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LotteryPoolItemRepository : DefaultRepository<T_LotteryPoolItem>, ILotteryPoolItemRepository
    {
        public Task<int> DeleteByLotteryPoolIdAsync(int lotteryPoolId)
        {
            return base.DeleteAsync("LotteryPoolId=@LotteryPoolId", param: new { LotteryPoolId = lotteryPoolId });
        }
    }
}
