using Application.Interfaces.Repositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;

namespace Infrastructure.Repositories
{
    public class SubjectRepository: ISubjectRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public SubjectRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> ExistsAsync(int subjectId, int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>(
                "sp_Subject_Exists",
                new
                {
                    SubjectId = subjectId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
