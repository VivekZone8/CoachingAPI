using Application.DTOs.Batches;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IBatchScheduleService
    {
        Task<int> CreateAsync(CreateBatchScheduleRequest request, int coachingId);
        Task<IEnumerable<BatchSchedule>> GetByBatchAsync(int batchId, int coachingId);
    }
}
