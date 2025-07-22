using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Online_Store.Models.Entities
{
    public class Cart
    {
        public string Id { get; set; }
        public List<CartItem> Items { get; set; } = new List<CartItem>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        [NotMapped]
        public decimal TotalPrice => Items.Sum(i => i.Price * i.Quantity);
        //public Cart Cart { get; set; }
    }
}
