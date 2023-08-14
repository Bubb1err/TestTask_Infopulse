

namespace TestTask_Infopulse.DataAccess.Repositories.Interfaces
{
    public interface IRepositoryFactory
    {
        IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class;
        IRepository GetRepository<TEntity, IRepository>(bool hasCustomRepository = true)
            where TEntity : class
            where IRepository : class, IGenericRepository<TEntity>;
    }
}
