using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class CategoryCreateDTO
    {
        [Required(ErrorMessage = "Название категории обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Название должно быть от 3 до 50 символов")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Описание не должно превышать 200 символов")]
        public string Description { get; set; }
    }
}
