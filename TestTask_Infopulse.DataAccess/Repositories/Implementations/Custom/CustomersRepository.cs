using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces.Custom;

namespace TestTask_Infopulse.DataAccess.Repositories.Implementations.Custom
{
    public sealed class CustomersRepository : GenericRepository<Customer>, ICustomersRepository
    {
        public CustomersRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
