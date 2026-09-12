using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

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
