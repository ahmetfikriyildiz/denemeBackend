namespace denemeBackend.DTOs
{
    public record InvoiceDto(
        Guid Id,
        Guid ClientId,
        Guid UserId,
        float Amount,
        string Currency,
        string? Description,
        DateTime IssuedAt,
        bool Paid
    );
} 