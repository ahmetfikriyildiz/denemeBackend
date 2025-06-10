using denemeBackend.DTOs;
using denemeBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace denemeBackend.Controllers;

/// <summary>
/// Controller for handling authentication and user management operations.
/// </summary>
[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IAuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    /// <summary>
    /// Authenticates a user and returns JWT tokens.
    /// </summary>
    /// <param name="request">The login credentials.</param>
    /// <returns>JWT access token and refresh token.</returns>
    /// <response code="200">Returns the JWT tokens.</response>
    /// <response code="401">If the credentials are invalid.</response>
    [HttpPost("login")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> Login(LoginRequest request)
    {
        try
        {
            _logger.LogInformation("Login attempt for user: {Email}", request.Email);
            var response = await _authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Failed login attempt for user: {Email}", request.Email);
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Registers a new user and returns JWT tokens.
    /// </summary>
    /// <param name="request">The registration information.</param>
    /// <returns>JWT access token and refresh token.</returns>
    /// <response code="200">Returns the JWT tokens.</response>
    /// <response code="400">If the registration information is invalid.</response>
    [HttpPost("register")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<TokenResponse>> Register(RegisterRequest request)
    {
        try
        {
            _logger.LogInformation("Registration attempt for user: {Email}", request.Email);
            var response = await _authService.RegisterAsync(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            _logger.LogWarning("Failed registration attempt for user: {Email}", request.Email);
            return BadRequest(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Refreshes the JWT access token using a refresh token.
    /// </summary>
    /// <param name="request">The refresh token.</param>
    /// <returns>New JWT access token and refresh token.</returns>
    /// <response code="200">Returns the new JWT tokens.</response>
    /// <response code="401">If the refresh token is invalid.</response>
    [HttpPost("refresh-token")]
    [ProducesResponseType(typeof(TokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<TokenResponse>> RefreshToken([FromBody] RefreshTokenRequest request)
    {
        try
        {
            _logger.LogInformation("Token refresh attempt");
            var response = await _authService.RefreshTokenAsync(request.RefreshToken);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            _logger.LogWarning("Failed token refresh attempt");
            return Unauthorized(new { message = ex.Message });
        }
    }

    /// <summary>
    /// Revokes a refresh token.
    /// </summary>
    /// <param name="request">The refresh token to revoke.</param>
    /// <returns>Success message.</returns>
    /// <response code="200">If the token was successfully revoked.</response>
    /// <response code="401">If the user is not authenticated.</response>
    [Authorize]
    [HttpPost("revoke-token")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> RevokeToken([FromBody] RefreshTokenRequest request)
    {
        _logger.LogInformation("Token revocation attempt by user: {UserId}", User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value);
        await _authService.RevokeTokenAsync(request.RefreshToken);
        return Ok(new { message = "Token revoked successfully" });
    }

    /// <summary>
    /// Gets the current user's information.
    /// </summary>
    /// <returns>The current user's information.</returns>
    /// <response code="200">Returns the user information.</response>
    /// <response code="401">If the user is not authenticated.</response>
    /// <response code="404">If the user is not found.</response>
    [Authorize]
    [HttpGet("me")]
    [ProducesResponseType(typeof(UserResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserResponse>> GetCurrentUser()
    {
        var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return Unauthorized();
        }

        try
        {
            _logger.LogInformation("User info request for user: {UserId}", userId);
            var user = await _authService.GetUserInfoAsync(userId);
            return Ok(user);
        }
        catch (KeyNotFoundException ex)
        {
            _logger.LogWarning("User not found: {UserId}", userId);
            return NotFound(new { message = ex.Message });
        }
    }
} 