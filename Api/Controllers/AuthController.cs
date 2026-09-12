using Application.DTOs.Auths;
using Application.Interfaces;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Infrastructure.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Api.Controllers;

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
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var response = await _authService.LoginAsync(request);

        if (response == null)
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });

        return Ok(response);
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