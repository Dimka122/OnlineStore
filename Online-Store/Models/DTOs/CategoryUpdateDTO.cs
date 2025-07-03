using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class CategoryUpdateDTO
    {
        [Required(ErrorMessage = "ID категории обязательно")]
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(200)]
        public string Description { get; set; }
    }
}
