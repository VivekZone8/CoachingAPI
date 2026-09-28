using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Enrollment
{
    public class EnrollStudentResponse
    {
        public int EnrollmentId { get; set; }
        public int BatchId { get; set; }
        public decimal TotalAgreedFee { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
