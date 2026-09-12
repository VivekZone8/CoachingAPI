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
    public class AdmissionRepository : IAdmissionRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public AdmissionRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateAsync(Admission admission)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_Admission_Create",
                new
                {
                    admission.CoachingId,
                    admission.StudentId,
                    admission.CourseId,
                    admission.BatchId,
                    admission.AdmissionDate,
                    admission.MonthlyFee,
                    admission.AdmissionFee,
                    admission.AdmissionFeePaid
                },
                commandType: CommandType.StoredProcedure);
        }
    }
}
