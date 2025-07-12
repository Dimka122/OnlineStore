using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Store.Models.Entities
{
    public class CartItem
    {
        public int Id { get; set; } // Добавляем первичный ключ
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string ImageUrl { get; set; }

        public string CartId { get; set; } // Внешний ключ
        public Cart Cart { get; set; }
    }
}
