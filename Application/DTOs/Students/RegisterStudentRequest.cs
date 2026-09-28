using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Students
{
    public class RegisterStudentRequest
    {
        public int CoachingId { get; set; }

        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }

        public string MobileNo { get; set; } = string.Empty;
        public string? ParentMobileNo { get; set; }
        public decimal RegistrationFee { get; set; } = 0;

        public string? AlternatePhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? Gender { get; set; }

        public string? FatherName { get; set; }
        public string? MotherName { get; set; }

        public string? Address { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? Pincode { get; set; }

        public string? ProfileImageUrl { get; set; }
    }
}
