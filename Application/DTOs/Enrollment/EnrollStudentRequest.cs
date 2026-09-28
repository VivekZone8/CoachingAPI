using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Enrollment
{
    public class EnrollStudentRequest
    {
        public int CoachingId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int BatchId { get; set; }
        public string EnrollmentType { get; set; } = string.Empty;
        // Example: "1,2,3"
        public string? SubjectIds { get; set; }
        public decimal DiscountAmount { get; set; } = 0.00m;
    }
}
