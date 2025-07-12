using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace OnlineStore.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(
            ApplicationDbContext context,
            IMapper mapper,
            ILogger<ProductsController> logger)
        {
            _context = context;
            _mapper = mapper;
            _logger = logger;
        }

        // GET: api/Products
        [HttpGet]
        public async Task<ActionResult<ApiResponse<IEnumerable<ProductDTO>>>> GetProducts(
            [FromQuery] int? categoryId = null,
            [FromQuery] string search = "")
        {
            try
            {
                var query = _context.Products
                    .Include(p => p.Category)
                    .AsQueryable();

                if (categoryId.HasValue)
                {
                    query = query.Where(p => p.CategoryId == categoryId);
                }

                if (!string.IsNullOrWhiteSpace(search))
                {
                    query = query.Where(p =>
                        p.Name.Contains(search) ||
                        p.Description.Contains(search));
                }

                var products = await query.ToListAsync();
                var result = _mapper.Map<List<ProductDTO>>(products);

                return Ok(new ApiResponse<IEnumerable<ProductDTO>>(result));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка товаров");
                return StatusCode(500, new ApiResponse("Ошибка сервера"));
            }
        }

        // GET: api/Products/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ApiResponse<ProductDTO>>> GetProduct(int id)
        {
            var product = await _context.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
            {
                return NotFound(new ApiResponse("Товар не найден"));
            }

            return Ok(new ApiResponse<ProductDTO>(_mapper.Map<ProductDTO>(product)));
        }

        // POST: api/Products
        [HttpPost]
        public async Task<ActionResult<ApiResponse<ProductDTO>>> CreateProduct(
            [FromBody] ProductCreateDTO productDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseError(ModelState));
            }

            try
            {
                var product = _mapper.Map<Product>(productDto);
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                var result = _mapper.Map<ProductDTO>(product);
                return CreatedAtAction(
                    nameof(GetProduct),
                    new { id = product.Id },
                    new ApiResponse<ProductDTO>(result));
            }
            catch (Exception ex) { 
                _logger.LogError(ex, "Ошибка при создании товара");
            return StatusCode(500, new ApiResponse("Ошибка при создании товара"));
            }
        }

            // DELETE: api/Products/5
            [HttpDelete("{id}")]
        public async Task<ActionResult<ApiResponse>> DeleteProduct(int id)
        {
            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound(new ApiResponse("Товар не найден"));
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();

            return Ok(new ApiResponse("Товар успешно удален"));
        }
        [HttpPut("{id}")]
        private async Task<IActionResult> UpdateProduct(int id, [FromBody] ProductUpdateDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest(new ApiResponse("ID в URL и теле запроса не совпадают"));
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiResponseError(ModelState)); // Правильное создание ошибки
            }

            try
            {
                var product = await _context.Products.FindAsync(id);
                if (product == null)
                {
                    return NotFound(new ApiResponse("Товар не найден"));
                }

                _mapper.Map(productDto, product);
                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении товара");
                return StatusCode(500, new ApiResponse("Ошибка сервера"));
            }
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.Id == id);
        }
    }
}