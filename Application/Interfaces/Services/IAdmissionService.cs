using Application.DTOs.Admission;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces.Services
{
    public interface IAdmissionService
    {
        Task<int> CreateAsync(
       CreateAdmissionRequest request,
       int coachingId);
    }
}
