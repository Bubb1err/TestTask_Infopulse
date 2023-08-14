using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.ProductQueries
{
    public class GetProductQuery : IRequest<ProductDTO>
    {
        public int Id { get; private set; }
        public GetProductQuery(int id)
        {
            Id = id;
        }
    }
}
