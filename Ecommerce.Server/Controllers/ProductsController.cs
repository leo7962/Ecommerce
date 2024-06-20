using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductsController : ControllerBase
{
    private readonly IProductService productService;

    public ProductsController(IProductService productService)
    {
        this.productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts(int pageNumber = 1, int pageSize = 10)
    {
        var products = await productService.GetAllProductAsync(pageNumber, pageSize);
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDTO>> GetProduct(int id)
    {
        var product = await productService.GetProductByIdAsync(id);
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO product)
    {
        try
        {
            var productCreated = await productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productCreated.IdProduct }, productCreated);
        }
        catch (DbUpdateException ex)
        {

            return StatusCode(500, "An error occurred while saving the product in the database." + " " + ex.Message);
        }
        catch (Exception ex)
        {
            throw new Exception("An unexpected error has occurred." + " " + ex.Message);
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, ProductDTO product)
    {
        if (id != product.IdProduct) return BadRequest();
        await productService.UpdateProductAsync(product);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await productService.DeleteProductAsync(id);
        return NoContent();
    }

    [HttpPost("{IdProduct}/categories/{IdCategory}")]
    public async Task<IActionResult> AddCategoryToProduct(int IdProduct, int IdCategory)
    {
        var result = await productService.AddCategoryToProductAsync(IdProduct, IdCategory);
        if (result) return Ok();
        return NotFound();
    }
}