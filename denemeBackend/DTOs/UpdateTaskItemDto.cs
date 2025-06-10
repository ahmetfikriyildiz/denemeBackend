using System.ComponentModel.DataAnnotations;
using denemeBackend.Models;

namespace denemeBackend.DTOs
{
    public class UpdateTaskItemDto
    {
        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public Models.TaskStatus Status { get; set; }

        public DateTime? DueDate { get; set; }
    }
} 