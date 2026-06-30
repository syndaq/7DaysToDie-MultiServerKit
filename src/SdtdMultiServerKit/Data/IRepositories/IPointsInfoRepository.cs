using IceCoffee.SimpleCRUD;
using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Points info repository interface
    /// </summary>
    public interface IPointsInfoRepository : IRepository<T_PointsInfo>
    {
        /// <summary>
        /// ByplayerIDAsynchronousGetpoints
        /// </summary>
        /// <param name="playerId">PlayerID</param>
        /// <returns>Points</returns>
        Task<int> GetPointsByIdAsync(string playerId);

        /// <summary>
        /// ByplayerIDAsynchronously get points dictionary from list
        /// </summary>
        /// <param name="playerIds">PlayerIDList</param>
        /// <returns>Pointsdictionary</returns>
        Task<IReadOnlyDictionary<string, int>> GetPointsByIdsAsync(IEnumerable<string> playerIds);

        /// <summary>
        /// ByplayerIDAsynchronousUpdatepoints
        /// </summary>
        /// <param name="playerId">PlayerID</param>
        /// <param name="points">Points</param>
        /// <returns>Updateafter points</returns>
        Task<int> ChangePointsAsync(string playerId, int points);

        /// <summary>
        /// Asynchronously get paginated points info
        /// </summary>
        /// <param name="dto">Paginationparameter</param>
        /// <returns>Pagination Points info</returns>
        Task<PagedDto<T_PointsInfo>> GetPagedListAsync(PaginationQueryDto dto);

        /// <summary>
        /// Asynchronousresetpoints
        /// </summary>
        /// <returns>Reset pointscount</returns>
        Task<int> ResetPointsAsync();

        /// <summary>
        /// Asynchronousresetsign-instate
        /// </summary>
        /// <returns>Number of reset sign-in states</returns>
        Task<int> ResetSignInAsync();
    }
}