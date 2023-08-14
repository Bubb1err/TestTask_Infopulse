

using System.ComponentModel.DataAnnotations;
using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.BLL.ViewModels
{
    public class CreateProductDTO
    {
        [Required]
        public int ProductNumber { get; set; }
        [Required]
        [MinLength(1)]
        public string ProductName { get; set; }
        [Required]
        public ProductCategoryDTO ProductCategory { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public int AvailableQuantity { get; set; }
        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public ProductSize ProductSize { get; set; }
    }
}
