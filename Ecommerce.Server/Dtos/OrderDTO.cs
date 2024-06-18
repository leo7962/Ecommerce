using Ecommerce.Server.Entities;

namespace Ecommerce.Server.Dtos
{
    public class OrderDTO
    {
        public int IdOrder { get; set; }
        public int IdCustomer { get; set; }
        public List<DetailOrder> detailOrders { get; set; }
    }
}
