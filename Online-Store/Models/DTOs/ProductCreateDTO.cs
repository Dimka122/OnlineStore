using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class ProductCreateDTO
    {
        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        public string Description { get; set; }

        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Url(ErrorMessage = "Invalid URL format")]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
