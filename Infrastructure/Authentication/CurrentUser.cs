using Application.Interfaces.Repositories;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Security.Claims;

namespace Infrastructure.Authentication
{
    public class CurrentUser : ICurrentUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUser(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private ClaimsPrincipal User =>
            _httpContextAccessor.HttpContext?.User
            ?? new ClaimsPrincipal();

        public int UserId
        {
            get
            {
                var value = User.FindFirst(ClaimTypes.NameIdentifier)?.Value
                            ?? User.FindFirst("sub")?.Value;

                return int.TryParse(value, out var id) ? id : 0;
            }
        }

        public int? CoachingId
        {
            get
            {
                var value = User.FindFirst("CoachingId")?.Value;

                return int.TryParse(value, out var id) ? id : null;
            }
        }

        public UserRole Role
        {
            get
            {
                var value = User.FindFirst(ClaimTypes.Role)?.Value;

                return Enum.TryParse<UserRole>(value, out var role)
                    ? role
                    : 0;
            }
        }

        public string? Email =>
            User.FindFirst(ClaimTypes.Email)?.Value;
    }
}