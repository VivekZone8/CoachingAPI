using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BatchStudent
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public int BatchId { get; set; }
        public int StudentId { get; set; }

        public DateTime JoiningDate { get; set; }
        public DateTime? LeavingDate { get; set; }

        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
