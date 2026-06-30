using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LevelGiftItemRepository : DefaultRepository<T_LevelGiftItem>, ILevelGiftItemRepository
    {
        public Task<int> DeleteByLevelGiftIdAsync(string levelGiftId)
        {
            return base.DeleteAsync("LevelGiftId=@LevelGiftId", param: new { LevelGiftId = levelGiftId });
        }
    }
}
