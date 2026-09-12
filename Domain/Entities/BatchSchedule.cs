using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class BatchSchedule
    {
        public int Id { get; set; }
        public int CoachingId { get; set; }
        public int BatchId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
