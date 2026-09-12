using Application.DTOs.Auths;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request,string ipAddress, string userAgent);
        Task<int> RegisterAsync(RegisterRequest request);
        Task LogoutAsync(long sessionLogId);
    }
}
