using Application.DTOs.Batches;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
   public interface IBatchStudentService
    {
        Task<int> AssignAsync(
      AssignStudentRequest request,
      int coachingId);

        Task<IEnumerable<BatchStudent>> GetStudentsAsync(
            int batchId,
            int coachingId);
    }
}
