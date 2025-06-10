namespace denemeBackend.DTOs;

public record LoginRequest(string Email, string Password);
public record RegisterRequest(string Email, string Password, string FirstName, string LastName);
public record TokenResponse(string AccessToken, string RefreshToken, DateTime ExpiresAt);
public record RefreshTokenRequest(string RefreshToken);
public record UserResponse(string Id, string Email, string FirstName, string LastName, List<string> Roles); 