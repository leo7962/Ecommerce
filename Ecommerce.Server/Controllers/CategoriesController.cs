using Ecommerce.Server.Dtos;
using Ecommerce.Server.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly ICategoryService categoryService;

    public CategoriesController(ICategoryService categoryService)
    {
        this.categoryService = categoryService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<CategoryDTO>>> GetCategory()
    {
        var categories = await categoryService.GetAllCategoriesAsync();
        return Ok(categories);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<CategoryDTO>> GetCategory(int id)
    {
        var category = await categoryService.GetCategoryByIdAsync(id);
        return Ok(category);
    }

    [HttpPost]
    public async Task<ActionResult<CategoryDTO>> PostCategory(CategoryDTO categoryDTO)
    {
        var categoryCreated = await categoryService.CreateCategoryAsync(categoryDTO);
        return CreatedAtAction(nameof(GetCategory), new { id = categoryDTO.Id }, categoryCreated);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutProduct(int id, CategoryDTO categoryDTO)
    {
        if (id != categoryDTO.Id) return BadRequest();
        await categoryService.UpdateCategoryAsync(categoryDTO);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        await categoryService.DeleteCategoryAsync(id);
        return NoContent();
    }
}