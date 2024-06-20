namespace Ecommerce.Server.Dtos;

public class OrderDTO
{
    public int IdOrder { get; set; }
    public int IdUser { get; set; }
    public List<ProductDTO> Products { get; set; }
}