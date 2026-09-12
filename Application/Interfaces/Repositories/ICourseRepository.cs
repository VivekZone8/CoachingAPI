using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface ICourseRepository
    {
        Task<bool> ExistsAsync(int courseId, int coachingId);
        //Task<int> CreateAsync(Course course);
        //Task<IEnumerable<Course>> GetAllAsync(int coachingId);
    }
}
