namespace Ecommerce.Server.Dtos
{
    public class ProductDTO
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public List<CategoryDTO> Categories { get; set; }
    }
}
