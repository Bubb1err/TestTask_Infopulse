using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly DbSet<T> _dbSet;
        public GenericRepository(DbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _dbSet = _dbContext.Set<T>();
        }
        public virtual void Create(T entity)
        {
            this._dbSet.Add(entity);
        }
        public virtual void AddRange(IEnumerable<T> entities)
        {
            this._dbContext.AddRange(entities);
        }
        public virtual async Task CreateAsync(T entity)
        {
            await this._dbSet.AddAsync(entity);
        }

        public virtual void Delete(T entity)
        {
            this._dbSet.Remove(entity);
        }
        public virtual IQueryable<T> GetAll(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool track = false)
        {
            return this.GetQuery(track, predicate, include);
        }

        public virtual async Task<T> GetFirstOrDefaultAsync(Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, bool track = true)
        {
            var query = this.GetQuery(track, predicate, include);
            return await query.FirstOrDefaultAsync();
        }

        public virtual void Update(T entity)
        {
            this._dbSet.Update(entity);
        }
        private IQueryable<T> GetQuery(bool track, Expression<Func<T, bool>>? predicate = null, Func<IQueryable<T>, IIncludableQueryable<T, object>>? include = null, Expression<Func<T, T>>? selector = null)
        {
            IQueryable<T> query = this._dbSet;
            if (!track)
            {
                query = query.AsNoTracking();
            }
            if (include != null)
            {
                query = include(query);
            }
            if (predicate != null)
            {
                query = query.Where(predicate);
            }
            if (selector != null)
            {
                query = query.Select(selector);
            }
            return query;
        }
    }
}
