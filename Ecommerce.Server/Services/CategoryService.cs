using AutoMapper;
using Ecommerce.Server.Data;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Entities;
using Ecommerce.Server.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Services;

public class CategoryService : ICategoryService
{
    private readonly DataContext context;
    private readonly IMapper mapper;

    public CategoryService(DataContext context, IMapper mapper)
    {
        this.context = context;
        this.mapper = mapper;
    }

    public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDTO)
    {
        var category = mapper.Map<Category>(categoryDTO);
        await context.Categories.AddAsync(category);
        await context.SaveChangesAsync();
        return mapper.Map<CategoryDTO>(category);
    }

    public async Task DeleteCategoryAsync(int id)
    {
        var category = await context.Categories.FindAsync(id);
        if (category != null)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync()
    {
        var categories = await context.Categories
            .Select(c => new CategoryDTO
            {
                IdCategory = c.IdCategory,
                Name = c.Name             
            }).ToListAsync();

        return mapper.Map<IEnumerable<CategoryDTO>>(categories);
    }

    public async Task<CategoryDTO> GetCategoryByIdAsync(int id)
    {
        var category = await context.Categories
            .Where(c => c.IdCategory == id)
            .Select(c => new CategoryDTO
            {
                IdCategory = c.IdCategory,
                Name= c.Name                
            }).FirstOrDefaultAsync();

        return mapper.Map<CategoryDTO>(category);
    }

    public async Task UpdateCategoryAsync(CategoryDTO categoryDTO)
    {
        var category = mapper.Map<Category>(categoryDTO);
        context.Categories.Update(category);
        await context.SaveChangesAsync();
    }
}