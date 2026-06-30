using Autofac;
using IceCoffee.SimpleCRUD;

namespace SdtdMultiServerKit.Data
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IRepository<TEntity> GetGenericRepository<TEntity>()
        {
            throw new NotImplementedException();
        }

        public TRepository GetRepository<TRepository>() where TRepository : class, IRepository
        {
            return (TRepository)((ICloneable)ModApi.ServiceContainer.Resolve<TRepository>()).Clone();
        }
    }
}