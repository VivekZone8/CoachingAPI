using Application.DTOs.Batches;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BatchScheduleService : IBatchScheduleService
    {
        private readonly IBatchRepository _batchRepository;

        public BatchScheduleService(IBatchRepository batchRepository)
        {
            _batchRepository = batchRepository;
        }

        public async Task<int> CreateAsync(
            CreateBatchScheduleRequest request,
            int coachingId)
        {
            if (request.StartTime >= request.EndTime)
                throw new ArgumentException(
                    "End time must be greater than start time.");

            var batch = await _batchRepository.GetByIdAsync(
                request.BatchId,
                coachingId);

            if (batch == null)
                throw new ArgumentException("Invalid batch.");

            var schedule = new BatchSchedule
            {
                CoachingId = coachingId,
                BatchId = request.BatchId,
                DayOfWeek = request.DayOfWeek,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                IsActive = true
            };

            return await _batchRepository.CreateScheduleAsync(schedule);
        }

        public async Task<IEnumerable<BatchSchedule>> GetByBatchAsync(
            int batchId,
            int coachingId)
        {
            var batch = await _batchRepository.GetByIdAsync(
                batchId,
                coachingId);

            if (batch == null)
                throw new ArgumentException("Invalid batch.");

            return await _batchRepository.GetSchedulesAsync(
                batchId,
                coachingId);
        }
    }
}
