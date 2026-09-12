using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface ITeacherRepository
    {
        Task<bool> ExistsAsync(int teacherId, int coachingId);
        Task<int> CreateAsync(Teacher teacher);
        Task<IEnumerable<Teacher>> GetAllAsync(int coachingId);
    }
}
