using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILevelGiftRepository : IRepository<T_LevelGift>
    {
        Task<int> ResetClaimStateAsync();

        Task<T_LevelGift?> GetByPlayerIdAsync(string playerId);

        Task<IEnumerable<T_LevelGift>> GetEligibleLevelGiftsAsync(int playerLevel);
    }
}
