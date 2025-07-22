namespace Online_Store.Models.DTOs
{
    public class CartResponseDto
    {
        public string Id {  get; set; }
        public List<CartItemDto> Items { get; set; }
        public decimal TotalPrice { get; set; }
        public int TotalItems => Items.Sum(i => i.Quantity);
       // public DateTime CreatedAt { get; set; }
    }
}
