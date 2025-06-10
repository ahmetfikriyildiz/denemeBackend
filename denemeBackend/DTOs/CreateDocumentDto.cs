using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class CreateDocumentDto
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string FileUrl { get; set; } = null!;
    }
} 