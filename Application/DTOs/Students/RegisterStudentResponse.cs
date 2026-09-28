using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Students
{
    public class RegisterStudentResponse
    {
        public int Success { get; set; }
        public int? StudentId { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
    }
}
