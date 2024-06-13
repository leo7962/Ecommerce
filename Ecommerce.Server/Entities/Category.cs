using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities
{
    public class Category
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public required List<CategoryProduct> categoryProducts { get; set; }
    }
}
