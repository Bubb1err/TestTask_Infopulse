using System.ComponentModel.DataAnnotations;

namespace TestTask_Infopulse.BLL.ViewModels
{
    public class EditOrderDTO
    {
        public int Id { get; set; }
        [Required]
        public int Status { get; set; }
        public string? Comment { get; set; }
        [Required]
        public int CustomerId { get; set; }
        public List<OrderedProductDTO> OrderedProducts { get; set; } = new List<OrderedProductDTO>();
    }
}
