using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities
{
    public class Category
    {
        [Key] public int Id { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<CategoryProduct> categoryProducts { get; set; }
    }
}
