using System.ComponentModel.DataAnnotations;

namespace Ecommerce.Server.Entities
{
    public class Order
    {
        [Key] public int Id { get; set; }
        public required Customer Customer { get; set; }
        public required List<Order> Orders { get; set; }
    }
}
