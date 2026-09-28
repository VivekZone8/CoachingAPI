using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
   
        public class StudentService : IStudentService
        {
            private readonly IStudentRepository _repository;

            public StudentService(
                IStudentRepository repository)
            {
                _repository = repository;
            }

            public async Task<RegisterStudentResponse> RegisterAsync(
                RegisterStudentRequest request)
            {
                return await _repository.RegisterAsync(request);
            }
        }
    
}
