using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public UserRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> EmailExistsAsync(string email)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>(
                "sp_User_EmailExists",
                new { Email = email },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(User user)
        {
            using var connection = _connectionFactory.CreateConnection();

            // Check only if the incoming user is an Admin
            if (user.Role == UserRole.Admin && user.CoachingId.HasValue)
            {
                const string checkQuery = @"
            SELECT COUNT(1) 
            FROM Users 
            WHERE CoachingId = @CoachingId AND Role = @Role";

                var adminExists = await connection.ExecuteScalarAsync<bool>(
                    checkQuery,
                    new
                    {
                        CoachingId = user.CoachingId.Value,
                        Role = (int)UserRole.Admin
                    });

                if (adminExists)
                {
                    return 0;
                    //throw new InvalidOperationException($"Coaching with ID {user.CoachingId} already has an assigned Admin.");
                }
            }

            return await connection.ExecuteScalarAsync<int>(
                "sp_User_Create",
                new
                {
                    user.CoachingId,
                    user.Name,
                    user.Email,
                    user.PasswordHash,
                    Role = (int)user.Role,
                    user.IsActive
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
