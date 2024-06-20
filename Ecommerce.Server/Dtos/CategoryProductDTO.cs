using Ecommerce.Server.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Dtos;

public class CategoryProductDTO
{
    [Key, Column(Order = 0), ForeignKey("Product")] public int IdProduct { get; set; }
    [Key, Column(Order = 1), ForeignKey("Category")] public int IdCategory { get; set; }
    public Category Category { get; set; }
}