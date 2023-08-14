using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom
{
    public class ProductCategoriesRepository : GenericRepository<ProductCategory>, IGenericRepository<ProductCategory>
    {
        public ProductCategoriesRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
