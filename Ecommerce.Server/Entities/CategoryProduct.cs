using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities;

public class CategoryProduct
{
    [Key]
    [Column(Order = 0)]
    [ForeignKey("Product")]
    public int IdProduct { get; set; }

    public Product Product { get; set; }

    [Key]
    [Column(Order = 1)]
    [ForeignKey("Category")]
    public int IdCategory { get; set; }

    public Category Category { get; set; }
}