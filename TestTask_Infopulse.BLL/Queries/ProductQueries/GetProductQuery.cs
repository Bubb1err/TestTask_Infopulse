using MediatR;
using TestTask_Infopulse.DataAccess.Entities;

namespace TestTask_Infopulse.BLL.Queries.ProductQueries
{
    public class GetProductQuery : IRequest<Product>
    {
        public int Id { get; private set; }
        public GetProductQuery(int id)
        {
            Id = id;
        }
    }
}
