using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<bool> EmailExistsAsync(string email);
        Task<int> CreateAsync(User user);

        Task<bool> CheckCoachingExistsAsync(int CoachingId);
    }
}
