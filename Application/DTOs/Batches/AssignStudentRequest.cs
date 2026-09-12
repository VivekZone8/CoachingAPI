using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Batches
{
    public class AssignStudentRequest
    {
        public int BatchId { get; set; }
        public int StudentId { get; set; }
        public DateTime JoiningDate { get; set; }
    }
}
