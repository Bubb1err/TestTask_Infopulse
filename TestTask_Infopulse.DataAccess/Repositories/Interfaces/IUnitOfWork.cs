

using Microsoft.EntityFrameworkCore;

namespace TestTask_Infopulse.DataAccess.Repositories.Interfaces
{
    public interface IUnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        TContext DbContext { get; }

    }
    public interface IUnitOfWork : IRepositoryFactory
    {
        int SaveChanges(bool ensureAutoHistory = false);
        Task<int> SaveChangesAsync(CancellationToken cancellationToken, bool ensureAutoHistory = false);
    }
}
