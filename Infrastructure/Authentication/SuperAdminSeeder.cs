using Application.Interfaces.Repositories;
using Dapper;
using Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Authentication
{
    public class SuperAdminSeeder
    {
        private readonly IDbConnectionFactory _connectionFactory;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IConfiguration _configuration;

        public SuperAdminSeeder(
            IDbConnectionFactory connectionFactory,
            IPasswordHasher passwordHasher,
            IConfiguration configuration)
        {
            _connectionFactory = connectionFactory;
            _passwordHasher = passwordHasher;
            _configuration = configuration;
        }

        public async Task SeedAsync()
        {
            var name = _configuration["SuperAdmin:Name"];
            var email = _configuration["SuperAdmin:Email"];
            var password = _configuration["SuperAdmin:Password"];

            if (string.IsNullOrWhiteSpace(name) ||
                string.IsNullOrWhiteSpace(email) ||
                string.IsNullOrWhiteSpace(password))
            {
                return;
            }

            using var connection = _connectionFactory.CreateConnection();

            var exists = await connection.ExecuteScalarAsync<bool>(
                """
            SELECT CAST(
                CASE WHEN EXISTS
                (
                    SELECT 1
                    FROM Users
                    WHERE Email = @Email
                      AND Role = 1
                )
                THEN 1 ELSE 0 END
            AS BIT)
            """,
                new { Email = email });

            if (exists)
                return;

            var passwordHash = _passwordHasher.Hash(password);

            await connection.ExecuteAsync(
                "sp_SuperAdmin_Create",
                new
                {
                    Name = name,
                    Email = email,
                    PasswordHash = passwordHash
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
