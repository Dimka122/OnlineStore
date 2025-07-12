using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class CartItemDto
    {
        [Required]
        public int ProductId { get; set; }

        [Range(1, 100)]
        public int Quantity { get; set; } = 1;
    }
}
