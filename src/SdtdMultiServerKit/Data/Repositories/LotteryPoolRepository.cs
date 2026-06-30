using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LotteryPoolRepository : DefaultRepository<T_LotteryPool>, ILotteryPoolRepository
    {
        public Task<IEnumerable<T_LotteryPool>> GetAllOrderByIdAsync()
        {
            return base.GetListAsync(orderByClause: "Id ASC");
        }

        public Task<IEnumerable<T_LotteryPool>> GetEnabledPoolsAsync()
        {
            return base.GetListAsync("IsEnabled=1", orderByClause: "Id ASC");
        }
    }
}
