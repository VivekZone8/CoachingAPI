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

        //public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        //{
        //    var user = await _authRepository.GetUserByEmailAsync(request.Email);

        //    if (user == null || !user.IsActive)
        //        return null;

        //    if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
        //        return null;

        //    var token = _jwtTokenService.GenerateToken(user);

        //    return new LoginResponse
        //    {
        //        AccessToken = token,
        //        ExpiresAt = DateTime.UtcNow.AddMinutes(30)
        //    };
        //}

        public async Task<LoginResponse> LoginAsync(LoginRequest request, string ipAddress, string userAgent)
        {
            var deviceType = ParseDeviceType(userAgent);
            var user = await _authRepository.GetUserByEmailAsync(request.Email);

            if (user == null)
            {
                return new LoginResponse { IsSuccess = false, Message = "Invalid email or password." };
            }

            if (!user.IsActive)
            {
                await _authRepository.InsertLoginLogAsync(user.Id, ipAddress, userAgent, deviceType, false, "Inactive Account");
                return new LoginResponse { IsSuccess = false, Message = "Your account is deactivated." };
            }

            // CHECK 1: Kya account blocked hai?
            if (user.LockoutEnd.HasValue && user.LockoutEnd.Value > DateTime.UtcNow)
            {
                var remainingMinutes = Math.Ceiling((user.LockoutEnd.Value - DateTime.UtcNow).TotalMinutes);

                await _authRepository.InsertLoginLogAsync(user.Id, ipAddress, userAgent, deviceType, false, "Account Blocked / Lockout Active");

                return new LoginResponse
                {
                    IsSuccess = false,
                    Message = $"Too many failed attempts. Your account is locked. Try again after {remainingMinutes} minutes."
                };
            }

            // CHECK 2: Password Match
            if (!_passwordHasher.Verify(request.Password, user.PasswordHash))
            {
                await _authRepository.HandleFailedAttemptAsync(user.Id, user.FailedLoginAttempts);

                await _authRepository.InsertLoginLogAsync(user.Id, ipAddress, userAgent, deviceType, false, "Invalid Password");

                int attemptsLeft = 5 - (user.FailedLoginAttempts + 1);
                string message = attemptsLeft > 0
                    ? $"Invalid password. {attemptsLeft} attempts remaining before account gets locked."
                    : "Your account is locked for 15 minutes due to 5 consecutive failed attempts.";

                return new LoginResponse { IsSuccess = false, Message = message };
            }

            // SUCCESSFUL LOGIN
            await _authRepository.ResetLockoutAsync(user.Id);

            // Audit Log Entry
            var logId = await _authRepository.InsertLoginLogAsync(user.Id, ipAddress, userAgent, deviceType, true, null);

            var token = _jwtTokenService.GenerateToken(user);

            return new LoginResponse
            {
                IsSuccess = true,
                Message = "Login successful.",
                AccessToken = token,
                ExpiresAt = DateTime.UtcNow.AddMinutes(30),
                SessionLogId = logId
            };
        }

        public async Task LogoutAsync(long sessionLogId)
        {
            await _authRepository.UpdateLogoutTimeAsync(sessionLogId);
        }

        private static string ParseDeviceType(string? userAgent)
        {
            if (string.IsNullOrWhiteSpace(userAgent)) return "Unknown";

            var ua = userAgent.ToLowerInvariant();
            if (ua.Contains("ipad") || ua.Contains("tablet")) return "Tablet";
            if (ua.Contains("mobile") || ua.Contains("android") || ua.Contains("iphone")) return "Mobile";
            return "Desktop / Web";
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
