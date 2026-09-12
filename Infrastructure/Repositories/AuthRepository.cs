using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AuthRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            SELECT Id, Name,CoachingId, Email, PasswordHash, Role, IsActive, CreatedAt,FailedLoginAttempts, LockoutEnd
            FROM Users
            WHERE Email = @Email
            """;

            return await connection.QueryFirstOrDefaultAsync<User>(
                sql,
                new { Email = email });
        }
        public async Task<int> CreateUserAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
        INSERT INTO Users (Name, Email, PasswordHash, Role, IsActive, CreatedAt)
        VALUES (@Name, @Email, @PasswordHash, @Role, @IsActive, @CreatedAt);
        SELECT CAST(SCOPE_IDENTITY() AS INT);
        """;

            return await connection.ExecuteScalarAsync<int>(sql, user);
        }

        // 1. Insert Login Attempt Log (Returns Generated Log Id)
        public async Task<long> InsertLoginLogAsync(int userId, string? ip, string? userAgent, string? device, bool isSuccess, string? reason)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            INSERT INTO UserLoginLogs (UserId, LoginTime, IpAddress, UserAgent, Device, IsSuccess, FailureReason)
            OUTPUT INSERTED.Id
            VALUES (@UserId, SYSUTCDATETIME(), @IpAddress, @UserAgent, @Device, @IsSuccess, @FailureReason);
        """;

            return await connection.ExecuteScalarAsync<long>(sql, new
            {
                UserId = userId,
                IpAddress = ip,
                UserAgent = userAgent,
                Device = device,
                IsSuccess = isSuccess,
                FailureReason = reason
            });
        }

        // 2. Failed attempt count badhana aur 5 cross hote hi lockout set karna
        public async Task HandleFailedAttemptAsync(int userId, int currentFailed)
        {
            using var connection = _connectionFactory.CreateConnection();

            const int maxAttempts = 5;
            const int lockoutMinutes = 15;

            DateTime? lockoutEnd = (currentFailed + 1 >= maxAttempts)
                ? DateTime.UtcNow.AddMinutes(lockoutMinutes)
                : null;

            const string sql = """
            UPDATE Users
            SET FailedLoginAttempts = FailedLoginAttempts + 1,
                LockoutEnd = CASE 
                    WHEN @LockoutEnd IS NOT NULL THEN @LockoutEnd 
                    ELSE LockoutEnd 
                END
            WHERE Id = @UserId;
        """;

            await connection.ExecuteAsync(sql, new { UserId = userId, LockoutEnd = lockoutEnd });
        }

        // 3. Successful login hone par counter clear karna
        public async Task ResetLockoutAsync(int userId)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            UPDATE Users
            SET FailedLoginAttempts = 0,
                LockoutEnd = NULL
            WHERE Id = @UserId;
        """;

            await connection.ExecuteAsync(sql, new { UserId = userId });
        }

        // 4. Logout stamp lagana
        public async Task UpdateLogoutTimeAsync(long sessionLogId)
        {
            using var connection = _connectionFactory.CreateConnection();

            const string sql = """
            UPDATE UserLoginLogs
            SET LogoutTime = SYSUTCDATETIME()
            WHERE Id = @SessionLogId;
        """;

            await connection.ExecuteAsync(sql, new { SessionLogId = sessionLogId });
        }
    }
}
