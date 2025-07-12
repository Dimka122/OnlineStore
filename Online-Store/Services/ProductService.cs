using Microsoft.EntityFrameworkCore;
using Online_Store.Data;
using Online_Store.Models.Entities;

namespace Online_Store.Services
{
    // Services/ProductService.cs
    public class ProductService : IProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await _context.Products
                .FirstOrDefaultAsync(p => p.Id == id)
                ?? throw new KeyNotFoundException("Product not found");
        }

        public async Task<bool> CheckProductStockAsync(int productId, int quantity)
        {
            var product = await GetProductByIdAsync(productId);
            return product.StockQuantity >= quantity;
        }
    }
}
