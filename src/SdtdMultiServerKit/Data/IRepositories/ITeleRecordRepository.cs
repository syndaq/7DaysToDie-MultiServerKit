using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    /// <summary>
    /// Teleport recordrepository
    /// </summary>
    public interface ITeleRecordRepository : IRepository<T_TeleRecord>
    {
        /// <summary>
        /// ByplayerIdGet the latest teleport record by target type
        /// </summary>
        /// <param name="playerId"></param>
        /// <param name="teleTargetType"></param>
        /// <returns></returns>
        Task<T_TeleRecord?> GetNewestAsync(string playerId, TeleTargetType teleTargetType);
    }
}