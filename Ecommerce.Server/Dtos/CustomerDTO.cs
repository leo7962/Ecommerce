namespace Ecommerce.Server.Dtos
{
    public class CustomerDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<int> IdOrders { get; set; }
    }
}
