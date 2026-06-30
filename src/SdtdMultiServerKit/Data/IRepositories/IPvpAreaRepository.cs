using IceCoffee.SimpleCRUD;
using SdtdMultiServerKit.Data.Entities;

namespace SdtdMultiServerKit.Data.IRepositories
{
    public interface IPvpAreaRepository : IRepository<T_PvpArea>
    {
        Task<int> DeleteAllAsync();
    }
}
