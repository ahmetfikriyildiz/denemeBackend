using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class CreateInvoiceDto
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        [Range(0, float.MaxValue)]
        public float Amount { get; set; }

        [Required]
        public string Currency { get; set; } = "USD";

        public string? Description { get; set; }
    }
} 