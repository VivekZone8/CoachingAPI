using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Teachers
{
    public class CreateTeacherRequest
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string? Phone { get; set; }
        public string? Qualification { get; set; }
    }
}
