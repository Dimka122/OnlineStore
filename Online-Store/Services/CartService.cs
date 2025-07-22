using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace Online_Store.Services
{

    public class CartService : ICartService
    {
        private readonly ApplicationDbContext _context;
        private readonly IProductService _productService;

        public CartService(ApplicationDbContext context, IProductService productService)
        {
            _context = context;
            _productService = productService;
        }

        public async Task<Cart> GetCartAsync(string cartId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId) ?? new Cart { Id = cartId };
        }

        public async Task<Cart> AddItemAsync(string cartId, CartItemDto itemDto)
        {

            

            // 1. Получаем или создаем корзину
            var cart = await _context.Carts
                .Include(c => c.Items)
                .FirstOrDefaultAsync(c => c.Id == cartId) ?? new Cart { Id = cartId };

            if (cart == null)
            {
                cart = new Cart
                {
                    Id = cartId,
                    CreatedAt = DateTime.UtcNow
                };
                _context.Carts.Add(cart);
                await _context.SaveChangesAsync(); // Сначала сохраняем корзину

                

            }

            // 2. Проверяем товар
            var product = await _productService.GetProductByIdAsync(itemDto.ProductId);

            // 3. Добавляем/обновляем товар
            var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == itemDto.ProductId);

            if (existingItem != null)
            {
                existingItem.Quantity += itemDto.Quantity;
            }
            else
            {
                cart.Items.Add(new CartItem
                {
                    ProductId = product.Id,
                    ProductName = product.Name,
                    Price = product.Price,
                    Quantity = itemDto.Quantity,
                    ImageUrl = product.ImageUrl,
                    CartId = cart.Id // Убедимся, что CartId установлен
                });
            }

            cart.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();

           return cart; 
        }

        public async Task<Cart> RemoveItemAsync(string cartId, int productId)
        {
            var cart = await GetCartAsync(cartId);
            var itemToRemove = cart.Items.FirstOrDefault(i => i.ProductId == productId);

            if (itemToRemove != null)
            {
                cart.Items.Remove(itemToRemove);
                _context.Carts.Update(cart);
                await _context.SaveChangesAsync();
            }

            return cart;
        }

        public async Task ClearCartAsync(string cartId)
        {
            var cart = await GetCartAsync(cartId);
            cart.Items.Clear();
            _context.Carts.Update(cart);
            await _context.SaveChangesAsync();
        }
    }
}

