using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class CreateUserDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string Name { get; set; } = null!;
    }
} 