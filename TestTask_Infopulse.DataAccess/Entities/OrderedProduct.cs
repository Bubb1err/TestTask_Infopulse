namespace TestTask_Infopulse.DataAccess.Entities
{
    public class OrderedProduct
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
