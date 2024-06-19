using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<DetailOrderDTO>> GetAllOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task<OrderDTO> CreateOrdedAsyunc(OrderDTO orderDTO);
    Task UpdateOrderAsync(int id, OrderDTO orderDTO);
    Task DeleteOrderAsync(int id);
}