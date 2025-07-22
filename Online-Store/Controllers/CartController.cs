using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Online_Store.Models.DTOs;
using Online_Store.Services;
using System.Security.Claims;

namespace Online_Store.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(ICartService cartService, IHttpContextAccessor httpContextAccessor)
        {
            _cartService = cartService;
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetCartId()
        {
            // Для авторизованных пользователей используем UserId
            if (_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                return _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            }

            // Для анонимов - идентификатор из куки
            return _httpContextAccessor.HttpContext.Request.Cookies["CartId"] ?? Guid.NewGuid().ToString();
        }

        [HttpGet]
        public async Task<ActionResult<CartResponseDto>> GetCart()
        {
            var cartId = GetCartId();
            var cart = await _cartService.GetCartAsync(cartId);

            return Ok(new CartResponseDto
            {
                Items = cart.Items.Select(i => new CartItemDto
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity
                }).ToList(),
                TotalPrice = cart.TotalPrice
            });
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDto itemDto)
        {
            try
            {
                var cartId = GetCartId();
                var cart = await _cartService.AddItemAsync(cartId, itemDto);

                if (!_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    Response.Cookies.Append("CartId", cartId, new CookieOptions
                    {
                        Expires = DateTime.Now.AddDays(30),
                        IsEssential = true
                    });
                }

                return Ok(cart);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        

        [HttpDelete("remove/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var cartId = GetCartId();
            await _cartService.RemoveItemAsync(cartId, productId);
            return NoContent();
        }
    }
}
