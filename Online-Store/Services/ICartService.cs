using Online_Store.Models.DTOs;
using Online_Store.Models.Entities;

namespace Online_Store.Services
{
    public interface ICartService
    {
        Task<Cart> GetCartAsync(string cartId);
        Task<Cart> AddItemAsync(string cartId, CartItemDto item);
        Task<Cart> RemoveItemAsync(string cartId, int productId);
        Task ClearCartAsync(string cartId);
    }
}
