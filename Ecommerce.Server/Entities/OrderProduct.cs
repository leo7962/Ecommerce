using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities;

public class OrderProduct
{
    [Key]
    [Column(Order = 0)]
    [ForeignKey("Order")]
    public int IdOrder { get; set; }

    public Order Order { get; set; }

    [Key]
    [Column(Order = 1)]
    [ForeignKey("Product")]
    public int IdProduct { get; set; }

    public Product Product { get; set; }
    public int Quantity { get; set; }
}