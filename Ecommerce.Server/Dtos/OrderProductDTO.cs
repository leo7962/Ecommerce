namespace Ecommerce.Server.Dtos;

public class OrderProductDTO
{
    public int IdProduct { get; set; }
    public int IdOrder { get; set; }
    public int Quantity { get; set; }
}