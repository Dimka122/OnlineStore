using Online_Store.Models.Entities;

namespace Online_Store.Services
{
    public interface IProductService
    {
        Task<Product> GetProductByIdAsync(int id);
        Task<bool> CheckProductStockAsync(int productId, int quantity);
    }
}
