using Application.DTOs.Auths;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

[EnableRateLimiting("login")]
[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;

    public AuthController(
        IAuthService authService,
        ICurrentUser currentUser
        )
    {
        _authService = authService;
        _currentUser = currentUser;
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        // 1. Client IP nikaalein (Reverse Proxy / Load Balancer ko bhi support karega)
        var ipAddress = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault()
                        ?? HttpContext.Connection.RemoteIpAddress?.ToString()
                        ?? "Unknown";

        // 2. User-Agent header (Browser / OS / Device info ke liye)
        var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

        // 3. Service call me IP aur User-Agent pass karein
        var response = await _authService.LoginAsync(request, ipAddress, userAgent);

        // 4. Agar login fail hua (Wrong password, Locked, ya Inactive)
        if (!response.IsSuccess)
        {
            return Unauthorized(new
            {
                message = response.Message
            });
        }

        return Ok(response);
    }

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(
    [FromBody] LogoutRequest request,
    [FromServices] IMemoryCache cache)
    {
        // 1. Database me logout entry mark karein
        await _authService.LogoutAsync(request.SessionLogId);

        // 2. Request se current token nikaalein
        var authHeader = Request.Headers["Authorization"].ToString();
        var token = authHeader.StartsWith("Bearer ", StringComparison.OrdinalIgnoreCase)
            ? authHeader.Substring("Bearer ".Length).Trim()
            : authHeader.Trim();

        // 3. Token ko cache me 30 minute (token ki remaining expiry) ke liye blacklist kar dein
        cache.Set($"blacklist:{token}", true, TimeSpan.FromMinutes(30));

        return Ok(new { success = true, message = "Logged out successfully. Token is now invalidated." });
    }

    [Authorize]
    [HttpGet("profile")]
    public IActionResult Profile()
    {
        return Ok(new
        {
            message = "You are authenticated.",
            userId = _currentUser.UserId,
            coachingId = _currentUser.CoachingId,
            email = _currentUser.Email,
            role = _currentUser.Role.ToString()
        });
    }
}