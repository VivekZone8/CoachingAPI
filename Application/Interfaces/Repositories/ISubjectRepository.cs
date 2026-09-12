using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public  interface ISubjectRepository
    {
        Task<bool> ExistsAsync(int subjectId, int coachingId);
        //Task<int> CreateAsync(Subject subject);
        //Task<IEnumerable<Subject>> GetAllAsync(int coachingId);
    }
}
