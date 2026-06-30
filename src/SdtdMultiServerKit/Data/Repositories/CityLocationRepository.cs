using SdtdMultiServerKit.Data;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// City locationrepository
    /// </summary>
    public class CityLocationRepository : DefaultRepository<T_CityLocation>, ICityLocationRepository
    {
        /// <inheritdoc/>
        public Task<T_CityLocation?> GetByNameAsync(string name)
        {
            return base.GetFirstOrDefaultAsync("CityName=@CityName", param: new { CityName = name });
        }
    }
}