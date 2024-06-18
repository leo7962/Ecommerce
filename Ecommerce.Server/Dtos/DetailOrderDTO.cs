namespace Ecommerce.Server.Dtos
{
    public class DetailOrderDTO
    {
        public int IdDetailOrder { get; set; }
        public int OrderId { get; set; }
        public int IdProduct { get; set; }
        public int Quantity { get; set; }
    }
}
