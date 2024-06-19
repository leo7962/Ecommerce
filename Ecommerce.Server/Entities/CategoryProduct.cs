using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities;

public class CategoryProduct
{
    [ForeignKey("Product")] public int IdProduct { get; set; }
    public Product Product { get; set; }
    [ForeignKey("Category")] public int IdCategory { get; set; }
    public Category Category { get; set; }
}