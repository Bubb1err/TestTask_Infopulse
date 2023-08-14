using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom
{
    public sealed class OrderedProductsRepository : GenericRepository<OrderedProduct>, IOrderedProductsRepository
    {
        public OrderedProductsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
