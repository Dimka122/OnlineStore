using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class ProductUpdateDTO
    {
        [Required(ErrorMessage = "ID товара обязательно")]
        public int Id { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [Range(0.01, 1000000)]
        public decimal Price { get; set; }

        [Range(0, int.MaxValue)]
        public int StockQuantity { get; set; }

        [Url]
        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
    }
}
