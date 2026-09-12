using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IBatchStudentRepository
    {
        Task<bool> ExistsAsync(int batchId, int studentId, int coachingId);
        Task<int> AssignAsync(BatchStudent batchStudent);
        Task<IEnumerable<BatchStudent>> GetStudentsAsync(int batchId, int coachingId);
    }
}
