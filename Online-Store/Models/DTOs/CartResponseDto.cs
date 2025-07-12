namespace Online_Store.Models.DTOs
{
    public class CartResponseDto
    {
        public List<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems => Items.Sum(i => i.Quantity);
    }
}
