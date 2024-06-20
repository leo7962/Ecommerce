using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities;

public class Order
{
    [Key] public int IdOrder { get; set; }
    [ForeignKey("User")] public int IdUser { get; set; }
    public User User { get; set; }
    public virtual ICollection<OrderProduct> OrderProducts { get; set; }
}