using Ecommerce.Server.Dtos;

namespace Ecommerce.Server.Interfaces;

public interface IProductService
{
    Task<IEnumerable<ProductDTO>> GetAllProductAsync();
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
    Task UpdateProductAsync(ProductDTO productDto);
    Task DeleteProductAsync(int id);
    Task<bool> AddCategoryToProductAsync(int IdProduct, int IdCategory);
}