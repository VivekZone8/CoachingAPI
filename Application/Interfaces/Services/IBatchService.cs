using Application.DTOs.Batches;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IBatchService
    {
        Task<int> CreateAsync(CreateBatchRequest request, int coachingId);
        Task<IEnumerable<Batch>> GetAllAsync(int coachingId);
        Task<Batch?> GetByIdAsync(int id, int coachingId);
    }
}
