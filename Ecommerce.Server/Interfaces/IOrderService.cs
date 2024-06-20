using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task UpdateOrderAsync(OrderDTO orderDTO);
    Task DeleteOrderAsync(int id);
    Task<OrderDTO> CreateOrUpdateOrderAsync(OrderDTO orderDTO, bool addProducts = false);
}