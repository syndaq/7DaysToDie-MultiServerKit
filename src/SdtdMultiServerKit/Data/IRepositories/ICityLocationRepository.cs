using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// City locationrepository
    /// </summary>
    public interface ICityLocationRepository : IRepository<T_CityLocation>
    {
        /// <summary>
        /// Get city location by name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<T_CityLocation?> GetByNameAsync(string name);
    }
}