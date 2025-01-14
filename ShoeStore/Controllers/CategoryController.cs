using Microsoft.AspNetCore.Mvc;
using ShoeStore.DTO;
using ShoeStore.Services;

namespace ShoeStore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly CategoryService _categoryService;

        public CategoryController(CategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryDTO>> GetCategoryById(int id)
        {
            CategoryDTO categoryDTO = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            return Ok(categoryDTO);
        }

        [HttpPost]
        public async Task<ActionResult<CategoryDTO>> CreateCategory([FromBody] CategoryDTO categoryDto)
        {
            await _categoryService.AddCategoryAsync(categoryDto);
            return CreatedAtAction(nameof(GetCategoryById), new { id = categoryDto.Id }, categoryDto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(int id, CategoryDTO categoryDto)
        {
            if (id != categoryDto.Id)
            {
                return BadRequest();
            }
            await _categoryService.UpdateCategoryAsync(categoryDto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            CategoryDTO categoryDTO = await _categoryService.GetCategoryByIdAsync(id);
            if (categoryDTO == null)
            {
                return NotFound();
            }
            await _categoryService.DeleteCategoryAsync(id);
            return NoContent();
        }

        [HttpGet("all")]
        public async Task<ActionResult<ICollection<CategoryDTO>>> GetAllCategories()
        {
            return Ok(await _categoryService.GetAllCategoriesAsync());
        }







    }
}
