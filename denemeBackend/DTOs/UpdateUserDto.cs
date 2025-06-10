using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class UpdateUserDto
    {
        [Required]
        public string Name { get; set; } = null!;
    }
} 