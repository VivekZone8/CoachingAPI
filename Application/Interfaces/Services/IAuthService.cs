using Application.DTOs.Auths;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IAuthService
    {
        Task<LoginResponse?> LoginAsync(LoginRequest request);
        Task<int> RegisterAsync(RegisterRequest request);
    }
}
