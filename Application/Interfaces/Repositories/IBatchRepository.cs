using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IBatchRepository
    {
        Task<int> CreateAsync(Batch batch);
        Task<Batch?> GetByIdAsync(int id, int coachingId);
        Task<IEnumerable<Batch>> GetAllAsync(int coachingId);
        Task<int> CreateScheduleAsync(BatchSchedule schedule);
        Task<IEnumerable<BatchSchedule>> GetSchedulesAsync(int batchId, int coachingId);
    }
}
