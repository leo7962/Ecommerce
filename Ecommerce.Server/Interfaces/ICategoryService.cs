using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface ICategoryService
{
    Task<IEnumerable<CategoryDTO>> GetAllCategoriesAsync();
    Task<CategoryDTO> GetCategoryByIdAsync(int id);
    Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDTO);
    Task UpdateCategoryAsync(CategoryDTO categoryDTO);
    Task DeleteCategoryAsync(int id);
}