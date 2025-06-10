using denemeBackend.Models;

namespace denemeBackend.DTOs
{
    public record TaskItemDto(
        Guid Id,
        Guid ClientId,
        Guid UserId,
        string Title,
        string? Description,
        Models.TaskStatus Status,
        DateTime? DueDate,
        DateTime CreatedAt
    );
} 