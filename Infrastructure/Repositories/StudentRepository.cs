using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using Dapper;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public  class StudentRepository:IStudentRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public StudentRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
       public async Task<RegisterStudentResponse> RegisterAsync(RegisterStudentRequest request)
        {
            using var connection =
              _connectionFactory.CreateConnection();
            var result = await connection.QueryFirstAsync<RegisterStudentResponse>(
              "sp_SaveOrUpdateStudent",
              new
              {
                  request.CoachingId,

                  StudentId = 0,

                  request.FirstName,
                  request.LastName,
                  request.MobileNo,
                  request.ParentMobileNo,
                  request.RegistrationFee,
                  request.AlternatePhoneNumber,
                  request.Email,
                  request.DateOfBirth,
                  request.Gender,
                  request.FatherName,
                  request.MotherName,
                  request.Address,
                  request.City,
                  request.State,
                  request.Pincode,
                  request.ProfileImageUrl
              },
              commandType: CommandType.StoredProcedure);

            return result;

        }
    }
}
