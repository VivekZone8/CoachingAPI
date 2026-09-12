using Application.Interfaces.Repositories;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using Dapper;
using System.Text;

namespace Infrastructure.Repositories
{
    public class BatchStudentRepository : IBatchStudentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BatchStudentRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<bool> ExistsAsync(
            int batchId,
            int studentId,
            int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>(
                "sp_BatchStudent_Exists",
                new
                {
                    BatchId = batchId,
                    StudentId = studentId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> AssignAsync(BatchStudent batchStudent)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_BatchStudent_Assign",
                new
                {
                    batchStudent.CoachingId,
                    batchStudent.BatchId,
                    batchStudent.StudentId,
                    batchStudent.JoiningDate
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<BatchStudent>> GetStudentsAsync(
            int batchId,
            int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<BatchStudent>(
                "sp_BatchStudent_GetStudents",
                new
                {
                    BatchId = batchId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
