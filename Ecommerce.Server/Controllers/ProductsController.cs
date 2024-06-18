using AutoMapper;
using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService productService;
        private readonly IMapper mapper;

        public ProductsController(IProductService productService, IMapper mapper)
        {
            this.productService = productService;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            var products = await productService.GetAllProductAsync();

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await productService.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> PostProduct(ProductDTO product)
        {
            var productCreated = await productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProduct), new { id = productCreated.Id }, productCreated);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutProduct(int id, ProductDTO product)
        {
            if (id != product.Id)
            {
                return BadRequest();
            }
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
            if (result)
            {
                return Ok();
            }
            return NotFound();
        }
    }
}
