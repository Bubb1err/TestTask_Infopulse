

using MediatR;
using TestTask_Infopulse.BLL.ViewModels;

namespace TestTask_Infopulse.BLL.Queries.ProductQueries
{
    public class GetProductCategoriesQuery : IRequest<List<ProductCategoryDTO>>
    {
    }
}
