using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestTask_Infopulse.DataAccess;

namespace TestTask_Infopulse.BLL.Extensions
{
    public static class RegisterDatabaseExtension
    {
        public static IServiceCollection AddSqlServer(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString)
            );
            return services;
        }
    }
}
