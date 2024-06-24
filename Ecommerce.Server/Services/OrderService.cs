using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services;

public class OrderService : IOrderService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public OrderService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<OrderProductDTO> CreateOrderAsync(OrderProductDTO orderDTO)
    {
        try
        {
            var product = await context.Products.FindAsync(orderDTO.IdProduct);
            if (product == null) throw new Exception($"The product with the ID: {orderDTO.IdProduct} does not exist.");

            if (product.Quantity < orderDTO.Quantity)
                throw new Exception($"Not enough stock for the product {orderDTO.IdProduct}");
            var idUser = 1002;
            var order = new Order
            {
                IdUser = idUser
            };
            await context.AddAsync(order);
            await context.SaveChangesAsync();

            var orderProduct = new OrderProduct
            {
                IdOrder = order.IdOrder,
                IdProduct = orderDTO.IdProduct,
                Quantity = orderDTO.Quantity
            };
            context.OrderProducts.Add(orderProduct);

            product.Quantity -= orderDTO.Quantity;
            context.Products.Update(product);

            await context.SaveChangesAsync();

            return mapper.Map<OrderProductDTO>(orderProduct);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }

    public async Task DeleteOrderAsync(int id)
    {
        var order = await context.Orders.FindAsync(id);
        if (order != null)
        {
            context.Orders.Remove(order);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
    {
        var orders = await context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .ThenInclude(p => p.CategoryProducts)
            .Select(o => new OrderDTO
            {
                IdOrder = o.IdOrder,
                IdUser = o.IdUser,
                // Assign other necessary properties
                Products = o.OrderProducts.Select(op => new ProductDTO
                {
                    IdProduct = op.Product.IdProduct,
                    Name = op.Product.Name,
                    Description = op.Product.Description,
                    Price = op.Product.Price,
                    Quantity = op.Quantity,
                    Categories = op.Product.CategoryProducts.Select(cp => new CategoryDTO
                    {
                        IdCategory = cp.Category.IdCategory,
                        Name = cp.Category.Name
                    }).ToList()
                }).ToList()
            })
            .ToListAsync();
        return mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        var order = await context.Orders
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Product)
            .FirstOrDefaultAsync(o => o.IdOrder == id);
        return mapper.Map<OrderDTO>(order);
    }

    public async Task UpdateOrderAsync(OrderDTO orderDTO)
    {
        var order = mapper.Map<Order>(orderDTO);
        context.Orders.Update(order);
        await context.SaveChangesAsync();
    }

    private Order CreateOrderWithProducts(OrderDTO orderDTO)
    {
        // Create a new order with the products included.
        var order = new Order
        {
            IdUser = orderDTO.IdUser,
            OrderProducts = orderDTO.Products.Select(p => new OrderProduct
            {
                IdProduct = p.IdProduct
            }).ToList()
        };

        return order;
    }
}