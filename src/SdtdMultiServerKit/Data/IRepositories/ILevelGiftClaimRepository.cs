using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILevelGiftClaimRepository : IRepository<T_LevelGiftClaim>
    {
        Task<bool> HasClaimedAsync(string playerId, string levelGiftId);

        Task<int> RecordClaimAsync(string playerId, string levelGiftId);
    }
}
