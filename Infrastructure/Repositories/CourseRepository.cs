using Application.Interfaces.Repositories;
using Dapper;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Repositories
{
    public class CourseRepository: ICourseRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public CourseRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> ExistsAsync(int courseId, int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<bool>(
                "sp_Course_Exists",
                new
                {
                    CourseId = courseId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }
        //Task<int> CreateAsync(Course course)
        //{
        //    return
        //}
        //Task<IEnumerable<Course>> GetAllAsync(int coachingId)
        //{
        //    return 
        //}
    }
}
