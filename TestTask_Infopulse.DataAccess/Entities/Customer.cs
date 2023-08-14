namespace TestTask_Infopulse.DataAccess.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CustomerName { get; set; }
        public string Address { get; set; } = string.Empty;
    }
}
