using Application.DTOs.Teachers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface ITeacherService
    {
        Task<int> CreateAsync(CreateTeacherRequest request, int coachingId);
        Task<IEnumerable<Domain.Entities.Teacher>> GetAllAsync(int coachingId);
    }
}
