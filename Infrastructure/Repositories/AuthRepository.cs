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
            SELECT Id, Name, Email, PasswordHash, Role, IsActive, CreatedAt
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
    }
}
