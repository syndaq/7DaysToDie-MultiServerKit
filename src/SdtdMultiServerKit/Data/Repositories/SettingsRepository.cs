using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class SettingsRepository : DefaultRepository<T_Settings>, ISettingsRepository
    {
        public SettingsRepository()
        {
        }
    }
}