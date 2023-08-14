using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations
{
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : DbContext
    {
        private bool _disposed;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TContext context)
        {
            DbContext = context ?? throw new ArgumentNullException(nameof(context));
            _repositories = new Dictionary<Type, object>();
        }
        public TContext DbContext { get; }

        public virtual IGenericRepository<TEntity> GetRepository<TEntity>(bool hasCustomRepository = false) where TEntity : class
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepo = DbContext.GetService<IGenericRepository<TEntity>>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(DbContext);
            }

            return (IGenericRepository<TEntity>)_repositories[type];
        }

        public virtual IRepository GetRepository<TEntity, IRepository>(bool hasCustomRepository = true)
            where TEntity : class
            where IRepository : class, IGenericRepository<TEntity>
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            if (hasCustomRepository)
            {
                var customRepo = DbContext.GetService<IRepository>();
                if (customRepo != null)
                {
                    return customRepo;
                }
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new GenericRepository<TEntity>(DbContext);
            }

            return (IRepository)_repositories[type];
        }

        public virtual int SaveChanges(bool ensureAutoHistory = false)
        {
            return DbContext.SaveChanges();
        }

        public virtual async Task<int> SaveChangesAsync(CancellationToken cancellationToken, bool ensureAutoHistory = false)
        {
            return await DbContext.SaveChangesAsync(cancellationToken);
        }

        public virtual void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _repositories?.Clear();

                    DbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}
