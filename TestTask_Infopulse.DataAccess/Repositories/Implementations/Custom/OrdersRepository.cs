using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom
{
    public sealed class OrdersRepository : GenericRepository<Order>, IOrdersRepository
    {
        public OrdersRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
