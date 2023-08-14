using TestTask_Infopulse.DataAccess.Entities.Enums;


namespace TestTask_Infopulse.DataAccess.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductNumber { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public string ProductName { get; set; } = string.Empty;
        public ProductCategory Category { get; set; }
        private int availableQuantity;
        public int AvailableQuantity
        {
            get => this.availableQuantity;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                this.availableQuantity = value;
            }
        }
        private decimal price;
        public decimal Price
        {
            get => this.price;
            set
            {
                if (value < 0) throw new ArgumentOutOfRangeException(nameof(value));
                this.price = value;
            }
        }
        public string Description { get; set; } = String.Empty;
        public ProductSize ProductSize { get; set; }
    }
}
