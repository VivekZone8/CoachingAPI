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
    public class BatchRepository : IBatchRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public BatchRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<int> CreateAsync(Batch batch)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_Batch_Create",
                new
                {
                    batch.CoachingId,
                    batch.CourseId,
                    batch.SubjectId,
                    batch.TeacherId,
                    batch.Name,
                    batch.StartDate,
                    batch.EndDate,
                    batch.MaxStudents
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<IEnumerable<Batch>> GetAllAsync(int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<Batch>(
                "sp_Batch_GetAll",
                new { CoachingId = coachingId },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<Batch?> GetByIdAsync(int id, int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QuerySingleOrDefaultAsync<Batch>(
                "sp_Batch_GetById",
                new
                {
                    Id = id,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }

        public async Task<int> CreateScheduleAsync(BatchSchedule schedule)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.ExecuteScalarAsync<int>(
                "sp_BatchSchedule_Create",
                new
                {
                    schedule.CoachingId,
                    schedule.BatchId,
                    DayOfWeek = (int)schedule.DayOfWeek,
                    schedule.StartTime,
                    schedule.EndTime
                },
                commandType: CommandType.StoredProcedure);
        }
        public async Task<IEnumerable<BatchSchedule>> GetSchedulesAsync(
    int batchId,
    int coachingId)
        {
            using var connection = _connectionFactory.CreateConnection();

            return await connection.QueryAsync<BatchSchedule>(
                "sp_BatchSchedule_GetByBatch",
                new
                {
                    BatchId = batchId,
                    CoachingId = coachingId
                },
                commandType: CommandType.StoredProcedure);
        }
    }
    }
