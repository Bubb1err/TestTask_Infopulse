using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask_Infopulse.DataAccess.Repositories.Implementations;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;

namespace TestTask_Infopulse.BLL.Extensions
{
    public static class AddUnitOfWorkExtension
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services) where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        public static IServiceCollection AddRepository<TEntity, TRepository>(this IServiceCollection services)
                 where TEntity : class
                 where TRepository : class, IGenericRepository<TEntity>
        {
            services.AddScoped<IGenericRepository<TEntity>, TRepository>();

            return services;
        }
        public static IServiceCollection AddRepository<TEntity, IRepository, Repository>(this IServiceCollection services)
                 where TEntity : class
                 where Repository : class, IRepository
                 where IRepository : class, IGenericRepository<TEntity>

        {
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
