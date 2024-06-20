using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities;

public class Category
{
    [Key] public int IdCategory { get; set; }
    [Required] public string Name { get; set; }
    public virtual ICollection<CategoryProduct> CategoryProducts { get; set; }
}