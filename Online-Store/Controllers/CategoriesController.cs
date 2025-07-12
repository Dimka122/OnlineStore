using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace Online_Store.Controllers
{
    
    [ApiController]
        [Route("api/[controller]")]
        public class CategoriesController : ControllerBase
        {
            private readonly ApplicationDbContext _context;
            private readonly IMapper _mapper;
            private readonly ILogger<CategoriesController> _logger;

            public CategoriesController(
                ApplicationDbContext context,
                IMapper mapper,
                ILogger<CategoriesController> logger)
            {
                _context = context;
                _mapper = mapper;
                _logger = logger;
            }

            // GET: api/Categories
            [HttpGet]
            public async Task<ActionResult<ApiResponse<IEnumerable<CategoryDTO>>>> GetCategories()
            {
                try
                {
                    var categories = await _context.Categories
                        .Include(c => c.Products)
                        .Select(c => new CategoryDTO
                        {
                            Id = c.Id,
                            Name = c.Name,
                            Description = c.Description,
                            ProductCount = c.Products.Count
                        })
                        .ToListAsync();

                    return Ok(new ApiResponse<IEnumerable<CategoryDTO>>(categories));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при получении списка категорий");
                    return StatusCode(500, new ApiResponse("Ошибка сервера"));
                }
            }

            // GET: api/Categories/5
            [HttpGet("{id}")]
            public async Task<ActionResult<ApiResponse<CategoryDetailsDTO>>> GetCategory(int id)
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    return NotFound(new ApiResponse("Категория не найдена"));
                }

                var result = _mapper.Map<CategoryDetailsDTO>(category);
                return Ok(new ApiResponse<CategoryDetailsDTO>(result));
            }

        // POST: api/Categories
        
        [HttpPost]
        
        public async Task<ActionResult<ApiResponse<CategoryDTO>>> CreateCategory(
                [FromBody] CategoryCreateDTO categoryDto)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiResponseError(ModelState));
                }

                try
                {
                    var category = _mapper.Map<Category>(categoryDto);
                    _context.Categories.Add(category);
                    await _context.SaveChangesAsync();

                    var result = _mapper.Map<CategoryDTO>(category);
                    return CreatedAtAction(
                        nameof(GetCategory),
                        new { id = category.Id },
                        new ApiResponse<CategoryDTO>(result));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Ошибка при создании категории");
                    return StatusCode(500, new ApiResponse("Ошибка при создании категории"));
                }
            }

            // PUT: api/Categories/5
            [HttpPut("{id}")]
            public async Task<IActionResult> UpdateCategory(
                int id,
                [FromBody] CategoryUpdateDTO categoryDto)
            {
                if (id != categoryDto.Id)
                {
                    return BadRequest(new ApiResponse("ID в URL и теле запроса не совпадают"));
                }

                var category = await _context.Categories.FindAsync(id);
                if (category == null)
                {
                    return NotFound(new ApiResponse("Категория не найдена"));
                }

                _mapper.Map(categoryDto, category);

                try
                {
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException ex)
                {
                    if (!CategoryExists(id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        _logger.LogError(ex, "Ошибка при обновлении категории");
                        return StatusCode(500, new ApiResponse("Ошибка при обновлении категории"));
                    }
                }

                return NoContent();
            }

            // DELETE: api/Categories/5
            [HttpDelete("{id}")]
            public async Task<ActionResult<ApiResponse>> DeleteCategory(int id)
            {
                var category = await _context.Categories
                    .Include(c => c.Products)
                    .FirstOrDefaultAsync(c => c.Id == id);

                if (category == null)
                {
                    return NotFound(new ApiResponse("Категория не найдена"));
                }

                if (category.Products.Any())
                {
                    return BadRequest(new ApiResponse("Невозможно удалить категорию с товарами"));
                }

                _context.Categories.Remove(category);
                await _context.SaveChangesAsync();

                return Ok(new ApiResponse("Категория успешно удалена"));
            }

            private bool CategoryExists(int id)
            {
                return _context.Categories.Any(e => e.Id == id);
            }
        }
    }
