using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Teacher
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty; 
        public string? Phone { get; set; }
        public string? Qualification { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
