using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Ecommerce.Server.Entities
{
    public class Order
    {
        [Key] public int Id { get; set; }
        [ForeignKey("Customer")] public int IdCustomer { get; set; }
        public required Customer Customer { get; set; }
        public virtual ICollection<DetailOrder> DetailOrders { get; set; }
    }
}
