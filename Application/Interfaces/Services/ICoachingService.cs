using Application.DTOs.Coaching;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface ICoachingService
    {
        Task<CoachingResponse> AddAsync(AddCoachingRequest request);
    }
}
