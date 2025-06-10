using System.ComponentModel.DataAnnotations;

namespace denemeBackend.DTOs
{
    public class UpdateDocumentDto
    {
        [Required]
        public string FileName { get; set; } = null!;

        [Required]
        public string FileUrl { get; set; } = null!;
    }
} 