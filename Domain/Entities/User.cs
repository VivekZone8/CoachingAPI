using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public int? CoachingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public UserRole Role { get; set; } 
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
