using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class CreateClientDto
    {
        [Required]
        public string Name { get; set; } = null!;

        [EmailAddress]
        public string? Email { get; set; }

        public string? Phone { get; set; }

        public string? CompanyName { get; set; }

        public string? Notes { get; set; }
    }
} 