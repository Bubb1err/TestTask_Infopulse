namespace TestTask_Infopulse.BLL.ViewModels
{
    public class OrderedProductDTO
    {
        public int? Id { get; set; }
        public int ProductId { get; set; }
        public ProductDTO? Product { get; set; }
        public int Quantity { get; set; }
    }
}
