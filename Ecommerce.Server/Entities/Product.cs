using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities
{
    public class Product
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Price { get; set; }
        public List<CategoryProduct> CategoryProducts { get; set; }
    }
}
