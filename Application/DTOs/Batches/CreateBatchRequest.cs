using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Batches
{
    public class CreateBatchRequest
    {
        public int CourseId { get; set; }
        public int? SubjectId { get; set; }
        public int? TeacherId { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxStudents { get; set; }
    }
}
