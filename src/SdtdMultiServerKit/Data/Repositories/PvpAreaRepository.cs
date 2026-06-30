using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class PvpAreaRepository : DefaultRepository<T_PvpArea>, IPvpAreaRepository
    {
        public Task<int> DeleteAllAsync()
        {
            return base.DeleteAsync("1=1");
        }
    }
}
