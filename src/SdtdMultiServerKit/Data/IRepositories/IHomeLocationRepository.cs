using IceCoffee.SimpleCRUD;
using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Homelocationrepository
    /// </summary>
    public interface IHomeLocationRepository : IRepository<T_HomeLocation>
    {
        /// <summary>
        /// ByplayerIdGethomelocation
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<IEnumerable<T_HomeLocation>> GetByPlayerIdAsync(string playerId);

        /// <summary>
        /// ByplayerIdGet home location by player and home name
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="homeName"></param>
        /// <returns></returns>
        Task<T_HomeLocation?> GetByPlayerIdAndHomeNameAsync(string playerId, string homeName);

        /// <summary>
        /// ByplayerIdDelete home location by player and home name
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="homeName"></param>
        /// <returns></returns>
        Task<int> DeleteByPlayerIdAndHomeNameAsync(string playerId, string homeName);

        /// <summary>
        /// ByplayerIdGethomelocationcount
        /// </summary>
        /// <param name="playerId"></param>
        /// <returns></returns>
        Task<int> GetRecordCountByPlayerIdIdAsync(string playerId);

        /// <summary>
        /// PaginationGetrecord
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<PagedDto<T_HomeLocation>> GetPagedListAsync(PaginationQueryDto dto);
    }
}