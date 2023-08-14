namespace TestTask_Infopulse.BLL.ViewModels
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; }
        public decimal TotalOrderedCost { get; set; }
        public int OrdersCount { get; set; }    
    }
}
