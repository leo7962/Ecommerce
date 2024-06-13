namespace Ecommerce.Server.Dtos
{
    public class OrderDTO
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public List<ProductDTO> Products { get; set; }
    }
}
