using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services
{
    public class OrderService : IOrderService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public OrderService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<OrderDTO> CreateOrUpdateOrderAsync(OrderDTO orderDTO, bool addProducts = false)
        {
            // Create or map the order depending on 'addProducts'.
            var order = addProducts ? CreateOrderWithProducts(orderDTO) : mapper.Map<Order>(orderDTO);

            // Add the command to the context and save the changes.
            await context.Orders.AddAsync(order);
            await context.SaveChangesAsync();

            // Map and return the DTO of the order.
            return mapper.Map<OrderDTO>(order);
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
                    // Assign other necessary properties
                    Products = o.OrderProducts.Select(op => new ProductDTO
                    {
                        IdProduct = op.Product.IdProduct,
                        Name = op.Product.Name,
                        Description = op.Product.Description,
                        Price = op.Product.Price,
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
    }
}
