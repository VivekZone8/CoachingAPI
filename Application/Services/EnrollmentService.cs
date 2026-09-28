using Application.DTOs.Enrollment;
using Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class EnrollmentService : IEnrollmentService
    {
        private readonly IEnrollmentRepository _repository;

        public EnrollmentService(
            IEnrollmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<EnrollStudentResponse> EnrollStudentAsync(
            EnrollStudentRequest request)
        {
            return await _repository.EnrollStudentAsync(request);
        }
    }
}
