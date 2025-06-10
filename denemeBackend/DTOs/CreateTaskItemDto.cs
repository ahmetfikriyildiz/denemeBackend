using System.ComponentModel.DataAnnotations;
using denemeBackend.Models;

namespace denemeBackend.DTOs
{
    public class CreateTaskItemDto
    {
        [Required]
        public Guid ClientId { get; set; }

        [Required]
        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public Models.TaskStatus Status { get; set; } = Models.TaskStatus.TODO;

        public DateTime? DueDate { get; set; }
    }
} 