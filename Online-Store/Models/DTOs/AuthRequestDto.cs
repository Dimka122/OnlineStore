using System.ComponentModel.DataAnnotations;

namespace Online_Store.Models.DTOs
{
    public class AuthRequestDto
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
