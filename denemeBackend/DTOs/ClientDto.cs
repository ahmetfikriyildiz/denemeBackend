namespace denemeBackend.DTOs
{
    public record ClientDto(
        Guid Id,
        Guid UserId,
        string Name,
        string? Email,
        string? Phone,
        string? CompanyName,
        string? Notes,
        DateTime CreatedAt
    );
} 