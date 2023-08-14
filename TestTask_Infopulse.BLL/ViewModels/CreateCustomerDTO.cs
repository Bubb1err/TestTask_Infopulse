using System.ComponentModel.DataAnnotations;

namespace TestTask_Infopulse.BLL.ViewModels
{
    public class CreateCustomerDTO
    {
        [Required]
        [MinLength(1)]
        public string CustomerName { get; set; }
        public string? Address { get; set; } 
    }
}
