using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class CourseSubject
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public int CourseId { get; set; }
        public int SubjectId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
