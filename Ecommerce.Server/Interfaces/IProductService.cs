﻿using Ecommerce.Server.Dtos;
using Ecommerce.Server.Helpers;

namespace Ecommerce.Server.Interfaces;

public interface IProductService
{
    Task<PaginatedList<ProductDTO>> GetAllProductAsync(int pageNumber, int pageSize);
    Task<ProductDTO> GetProductByIdAsync(int id);
    Task<ProductDTO> CreateProductAsync(ProductDTO productDto);
    Task UpdateProductAsync(ProductDTO productDto);
    Task DeleteProductAsync(int id);
    Task<bool> AddCategoryToProductAsync(int IdProduct, int IdCategory);
}