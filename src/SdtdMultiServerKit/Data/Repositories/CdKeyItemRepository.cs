using SdtdMultiServerKit.Data.Entities;
using SdtdMultiServerKit.Data.IRepositories;

namespace SdtdMultiServerKit.Data.Repositories
{
    /// <summary>
    /// Represents a Cd Key Item repository.
    /// </summary>
    public class CdKeyItemRepository : DefaultRepository<CdKeyItem>, ICdKeyItemRepository
    {
        public Task<int> DeleteByCdKeyIdAsync(int cdKeyId)
        {
            return base.DeleteAsync("CdKeyId=@CdKeyId", param: new { CdKeyId = cdKeyId });
        }
    }
}