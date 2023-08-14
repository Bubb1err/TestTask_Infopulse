using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using TestTask_Infopulse.BLL.Queries.ProductQueries;
using TestTask_Infopulse.BLL.ViewModels;
using TestTask_Infopulse.DataAccess.Entities;
using TestTask_Infopulse.DataAccess.Repositories.Interfaces;

namespace TestTask_Infopulse.BLL.Handlers.ProductHandlers
{
    internal class GetProductCategoriesHandler : IRequestHandler<GetProductCategoriesQuery, List<ProductCategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<ProductCategory> _productCategoriesRepository;
        private readonly IMapper _mapper;

        public GetProductCategoriesHandler(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _productCategoriesRepository = _unitOfWork.GetRepository<ProductCategory>();
        }
        public async Task<List<ProductCategoryDTO>> Handle(GetProductCategoriesQuery request, CancellationToken cancellationToken)
        {
            var productCategories = await _productCategoriesRepository.GetAll().ToListAsync();
            var productCategoriesDto = _mapper.Map<List<ProductCategoryDTO>>(productCategories);
            return productCategoriesDto;
        }
    }
}
