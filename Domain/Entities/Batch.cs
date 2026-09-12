using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Batch
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public int CourseId { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }

        public string Name { get; set; } = string.Empty;

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }

        public string? Days { get; set; }

        public int? MaxStudents { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
