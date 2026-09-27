using Application.DTOs.Coaching;
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
    public class CoachingRepository:ICoachingRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
       public CoachingRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<CoachingResponse?> AddAsync(Coaching coaching)
        {
            using var connection = _connectionFactory.CreateConnection();

            var result = await connection.QueryFirstOrDefaultAsync<CoachingResponse>(
                "Proc_AddCoaching",
                new
                {
                    coaching.Name,
                    coaching.Code,
                    coaching.Email,
                    coaching.Phone,
                    coaching.Address
                },
                commandType: CommandType.StoredProcedure);

            return result;
        }
    }
}
