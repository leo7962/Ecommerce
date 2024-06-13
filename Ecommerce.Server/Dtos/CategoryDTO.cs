namespace Ecommerce.Server.Dtos
{
    public class CategoryDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public required List<ProductDTO> Products { get; set; }
    }
}
