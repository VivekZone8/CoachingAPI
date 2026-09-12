using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface ICurrentUser
    {
        int UserId { get; }
        int? CoachingId { get; }
        UserRole Role { get; }
        string? Email { get; }
    }
}
