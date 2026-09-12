using Application.DTOs.Auths;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;


namespace Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepository _authRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;
        public AuthService(IAuthRepository authRepository, IPasswordHasher passwordHasher, IJwtTokenService jwtTokenService)
        {
            _authRepository = authRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            if (user == null || !user.IsActive)
                return null;

            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
                return null;

            var token = _jwtTokenService.GenerateToken(user);

            return new LoginResponse
            {
                AccessToken = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30)
            };
        }
        public async Task<int> RegisterAsync(RegisterRequest request)
        {
            var existingUser = await _authRepository.GetUserByEmailAsync(request.Email);

            if (existingUser != null)
                throw new Exception("Email already registered.");

            var user = new User
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = UserRole.Admin,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            return await _authRepository.CreateUserAsync(user);
        }
    }
}
