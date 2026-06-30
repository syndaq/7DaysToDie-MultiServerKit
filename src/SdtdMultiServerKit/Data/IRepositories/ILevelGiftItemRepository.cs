using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILevelGiftItemRepository : IRepository<T_LevelGiftItem>
    {
        Task<int> DeleteByLevelGiftIdAsync(string levelGiftId);
    }
}
