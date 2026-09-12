using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Subject
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
