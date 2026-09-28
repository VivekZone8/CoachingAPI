using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Coaching
{
    public class AddCoachingRequest
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Address { get; set; }
      
    }
}
