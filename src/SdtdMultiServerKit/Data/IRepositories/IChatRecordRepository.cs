using IceCoffee.SimpleCRUD;
using IceCoffee.SimpleCRUD.Dtos;
using SdtdMultiServerKit.Data.Dtos;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface IChatRecordRepository : IRepository<T_ChatRecord>
    {
        Task<PagedDto<T_ChatRecord>> GetPagedListAsync(ChatRecordQueryDto dto);
    }
}