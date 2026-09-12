using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Batches
{
    public class CreateBatchScheduleRequest
    {
        public int BatchId { get; set; }
        public DayOfWeek DayOfWeek { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
    }
}
