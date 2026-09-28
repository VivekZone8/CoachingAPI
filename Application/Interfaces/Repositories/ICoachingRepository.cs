using Application.DTOs.Coaching;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface ICoachingRepository
    {
        Task<CoachingResponse> AddAsync(Coaching coaching);
    }
}
