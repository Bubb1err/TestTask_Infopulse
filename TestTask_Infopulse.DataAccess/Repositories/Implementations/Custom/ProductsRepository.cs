using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom
{
    public sealed class ProductsRepository : GenericRepository<Product>, IProductsRepository
    {
        public ProductsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
