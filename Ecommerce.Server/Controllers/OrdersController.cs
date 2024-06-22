using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrdersController : ControllerBase
{
    private readonly IOrderService orderService;

    public OrdersController(IOrderService orderService)
    {
        this.orderService = orderService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<OrderDTO>>> GetOrders()
    {
        var orders = await orderService.GetAllOrdersAsync();
        return Ok(orders);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<OrderDTO>> GetOrder(int id)
    {
        var order = await orderService.GetOrderByIdAsync(id);
        return Ok(order);
    }

    [HttpPost]
    public async Task<ActionResult> PostOrder([FromBody] OrderProductDTO orderDTO)
    {
        try
        {
            var ordercreated = await orderService.CreateOrderAsync(orderDTO);
            return Ok(ordercreated);
        }
        catch (DbUpdateException ex)
        {
            return StatusCode(500, "An error occurred while saving the product in the database." + " " + ex.Message);
        }
        catch (Exception ex)
        {
            BadRequest(new { message = "There is not enough stock to make the purchase." });
            return StatusCode(500, "An unexpected error has occurred." + " " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> PutOrder(int id, OrderDTO orderDTO)
    {
        if (id != orderDTO.IdOrder) return BadRequest();
        await orderService.UpdateOrderAsync(orderDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteOrder(int id)
    {
        await orderService.DeleteOrderAsync(id);
        return NoContent();
    }
}