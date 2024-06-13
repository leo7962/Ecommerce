using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services
{
    public class ProductService : IProductService
    {
        private readonly DataContext context;
        private readonly IMapper mapper;

        public ProductService(DataContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
        {
            var product = mapper.Map<Product>(productDto);
            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return mapper.Map<ProductDTO>(product);
        }

        public async Task DeleteProductAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            if (product == null)
            {
                context.Products.Remove(product);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
        {
            var products = await context.Products.ToListAsync();
            return mapper.Map<IEnumerable<ProductDTO>>(products);
        }

        public async Task<ProductDTO> GetProductByIdAsync(int id)
        {
            var product = await context.Products.FindAsync(id);
            return mapper.Map<ProductDTO>(product);
        }

        public async Task UpdateProductAsync(ProductDTO productDto)
        {
            var product = mapper.Map<Product>(productDto);
            context.Products.Update(product);
            await context.SaveChangesAsync();
        }
    }
}
