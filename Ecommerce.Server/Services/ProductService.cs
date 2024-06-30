using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services;

public class ProductService : IProductService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public ProductService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<bool> AddCategoryToProductAsync(int IdProduct, int IdCategory)
    {
        var product = await context.Products.FindAsync(IdProduct);
        var category = await context.Categories.FindAsync(IdCategory);

        if (product == null || category == null) return false;

        var categoryProduct = new CategoryProduct
        {
            IdProduct = IdProduct,
            IdCategory = IdCategory
        };

        context.CategoryProducts.Add(categoryProduct);

        await context.SaveChangesAsync();
        return true;
    }

    public async Task<ProductDTO> CreateProductAsync(ProductDTO productDto)
    {
        try
        {
            var product = mapper.Map<Product>(productDto);

            await context.Products.AddAsync(product);
            await context.SaveChangesAsync();
            return mapper.Map<ProductDTO>(product);
        }
        catch (DbUpdateException ex)
        {
            throw new Exception("An error occurred while saving the product in the database." + " " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error has occurred." + " " + ex.Message);
        }
    }

    public async Task DeleteProductAsync(int id)
    {
        var product = await context.Products.FindAsync(id);
        if (product != null)
        {
            context.Products.Remove(product);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ProductDTO>> GetAllProductAsync()
    {
        var count = await context.Products.CountAsync();

        var products = await context.Products
            .Include(p => p.CategoryProducts)
            .ThenInclude(cp => cp.Category)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Order)
            .ToListAsync();

        var productsDto = mapper.Map<IEnumerable<ProductDTO>>(products);
        return productsDto.ToList();
    }

    public async Task<ProductDTO> GetProductByIdAsync(int id)
    {
        var product = await context.Products
            .Include(p => p.CategoryProducts)
            .ThenInclude(cp => cp.Category)
            .Include(o => o.OrderProducts)
            .ThenInclude(op => op.Order)
            .FirstOrDefaultAsync(p => p.IdProduct == id);
        return mapper.Map<ProductDTO>(product);
    }

    public async Task UpdateProductAsync(ProductDTO productDto)
    {
        var product = mapper.Map<Product>(productDto);
        context.Products.Update(product);
        await context.SaveChangesAsync();
    }
}