using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Admission
    {
        public int Id { get; set; }
        public int CoachingId { get; set; } 
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? BatchId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal AdmissionFee { get; set; }
        public bool AdmissionFeePaid { get; set; }
        public DateTime FirstFeeDueDate { get; set; }
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }
    }
}
