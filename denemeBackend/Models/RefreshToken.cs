namespace denemeBackend.Models;

public class RefreshToken
{
    public string Token { get; set; } = string.Empty;
    public string UserId { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public bool IsRevoked { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string? ReplacedByToken { get; set; }
    public string? ReasonRevoked { get; set; }

    public virtual ApplicationUser User { get; set; } = null!;
} 