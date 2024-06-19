using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities;

public class DetailOrder
{
    public int Id { get; set; }
    [ForeignKey("Order")] public int IdOrder { get; set; }
    public Order Order { get; set; }
    [ForeignKey("Product")] public int IdProduct { get; set; }
    public Product Product { get; set; }
    public int Quantity { get; set; }
}