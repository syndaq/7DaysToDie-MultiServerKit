using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    public class LevelGiftCommandRepository : DefaultRepository<T_LevelGiftCommand>, ILevelGiftCommandRepository
    {
        public Task<int> DeleteByLevelGiftIdAsync(string levelGiftId)
        {
            return base.DeleteAsync("LevelGiftId=@LevelGiftId", param: new { LevelGiftId = levelGiftId });
        }
    }
}
