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
    public class TeacherRepository : ITeacherRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public TeacherRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExistsAsync(int teacherId, int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>(
                "sp_Teacher_Exists",
                new
                {
                    TeacherId = teacherId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateAsync(Teacher teacher)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Teacher>> GetAllAsync(int coachingId)
        {
            throw new NotImplementedException();
        }
    }
}
