namespace denemeBackend.DTOs
{
    public record UserDto(
        Guid Id,
        string Email,
        string Name,
        DateTime CreatedAt
    );
} 