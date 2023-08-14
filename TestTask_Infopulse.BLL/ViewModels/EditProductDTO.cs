using System.ComponentModel.DataAnnotations;
using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.BLL.ViewModels
{
    public class EditProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MinLength(1)]
        public string ProductName { get; set; }
        [Required]
        public ProductCategoryDTO ProductCategory { get; set; }
        [Required]
        public int AvailableQuantity { get; set; }
        [Required]
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        [Required]
        public ProductSize ProductSize { get; set; }
    }
}
