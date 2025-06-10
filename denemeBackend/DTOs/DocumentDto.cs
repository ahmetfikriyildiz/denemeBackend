namespace denemeBackend.DTOs
{
    public record DocumentDto(
        Guid Id,
        Guid ClientId,
        string FileName,
        string FileUrl,
        DateTime UploadedAt
    );
} 