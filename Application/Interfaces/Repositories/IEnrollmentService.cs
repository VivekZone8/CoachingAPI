using Application.DTOs.Enrollment;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IEnrollmentService
    {
        Task<EnrollStudentResponse> EnrollStudentAsync(
            EnrollStudentRequest request);
    }
}
