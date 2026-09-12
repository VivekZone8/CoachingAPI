using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Admission
{
    public class CreateAdmissionRequest
    {
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public int? BatchId { get; set; }
        public DateTime AdmissionDate { get; set; }
        public decimal MonthlyFee { get; set; }
        public decimal AdmissionFee { get; set; }
        public bool AdmissionFeePaid { get; set; }
    }
}
