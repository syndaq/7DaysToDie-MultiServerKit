using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface ILevelGiftCommandRepository : IRepository<T_LevelGiftCommand>
    {
        Task<int> DeleteByLevelGiftIdAsync(string levelGiftId);
    }
}
