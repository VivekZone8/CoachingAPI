using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Student
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime AdmissionDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
