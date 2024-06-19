namespace Ecommerce.Server.Dtos;

public class ProductDTO
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public List<int> IdCategories { get; set; }
}