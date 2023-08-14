using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.BLL.ViewModels
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public int ProductNumber { get; set; }
        public string ProductName { get; set; }
        public ProductCategoryDTO ProductCategory { get; set; }
        public int AvailableQuantity { get; set; }
        public decimal Price { get; set; }
        public ProductSize ProductSize { get; set; }
    }
}
