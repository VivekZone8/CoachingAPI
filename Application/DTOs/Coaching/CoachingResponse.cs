using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Coaching
{
    public class CoachingResponse
    {
        public int Success { get; set; }
        public string Message { get; set; } = string.Empty;

        public int? Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
