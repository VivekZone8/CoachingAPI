using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IAuthRepository
    {
        Task<User?> GetUserByEmailAsync(string email);
        Task<int> CreateUserAsync(User user);
        Task<long> InsertLoginLogAsync(int userId, string? ipAddress, string? userAgent, string? device, bool isSuccess, string? failureReason);
        Task HandleFailedAttemptAsync(int userId, int currentFailedAttempts);
        Task ResetLockoutAsync(int userId);
        Task UpdateLogoutTimeAsync(long sessionLogId);
    }
}
