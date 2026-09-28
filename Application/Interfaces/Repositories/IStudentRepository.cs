using Application.DTOs.Students;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Repositories
{
    public interface IStudentRepository
    {
        Task<RegisterStudentResponse> RegisterAsync(RegisterStudentRequest request);
    }
}
