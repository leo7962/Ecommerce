namespace Ecommerce.Server.Dtos;

public class ProductDTO
{
    public int IdProduct { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public List<CategoryDTO> Categories { get; set; }    
}