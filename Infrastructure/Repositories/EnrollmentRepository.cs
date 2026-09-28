using Application.DTOs.Enrollment;
using Application.Interfaces.Repositories;
using Dapper;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EnrollmentRepository : IEnrollmentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public EnrollmentRepository(
            IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<EnrollStudentResponse> EnrollStudentAsync(
            EnrollStudentRequest request)
        {   
            using var connection =
                _connectionFactory.CreateConnection();

            var result = await connection.QueryFirstAsync<EnrollStudentResponse>(
                "sp_EnrollStudent",
                new
                {
                    request.CoachingId,
                    request.StudentId,
                    request.CourseId,
                    request.BatchId,
                    request.EnrollmentType,
                    request.SubjectIds,
                    request.DiscountAmount
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
  }
