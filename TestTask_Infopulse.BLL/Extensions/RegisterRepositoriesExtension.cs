using Microsoft.Extensions.DependencyInjection;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.BLL.Extensions
{
    public static class RegisterRepositoriesExtension
    {
        public  static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddRepository<Order, IOrdersRepository, OrdersRepository>()
                .AddRepository<Customer, ICustomersRepository, CustomersRepository>()
                .AddRepository<Product, IProductsRepository, ProductsRepository>()
                .AddRepository<OrderedProduct, IOrderedProductsRepository, OrderedProductsRepository>()
                .AddRepository<ProductCategory, ProductCategoriesRepository>();
            return services;
        }
    }
}
