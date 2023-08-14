using TestTask_Infopulse.DataAccess.Entities.Enums;

namespace TestTask_Infopulse.DataAccess.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public int OrderNumber { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.Now;
        public Status Status { get; set; }
        private decimal totalCost;
        public decimal TotalCost => totalCost;
        public string Comment { get; set; } = string.Empty;
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public ICollection<OrderedProduct> OrderedProducts { get; set; } = new List<OrderedProduct>();
        public void UpdateTotalCost()
        {
            decimal sum = 0;
            foreach (var orderedProduct in this.OrderedProducts)
            {
                sum += orderedProduct.Product.Price * orderedProduct.Quantity;
            }
            this.totalCost = sum;
        }
    }
}
