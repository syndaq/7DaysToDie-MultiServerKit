using SdtdMultiServerKit.Data;
using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Teleport recordrepository
    /// </summary>
    public class TeleRecordRepository : DefaultRepository<T_TeleRecord>, ITeleRecordRepository
    {
        /// <inheritdoc/>
        public Task<T_TeleRecord?> GetNewestAsync(string playerId, TeleTargetType teleTargetType)
        {
            return base.GetFirstOrDefaultAsync(
                "PlayerId=@PlayerId AND TargetType=@TargetType", 
                "CreatedAt DESC LIMIT 1", 
                param: new { PlayerId = playerId, TargetType = teleTargetType.ToString() });
        }
    }
}