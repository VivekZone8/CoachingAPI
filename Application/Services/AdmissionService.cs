using Application.DTOs.Admission;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class AdmissionService : IAdmissionService
    {
        private readonly IAdmissionRepository _admissionRepository;

        public AdmissionService(
            IAdmissionRepository admissionRepository)
        {
            _admissionRepository = admissionRepository;
        }

        public async Task<int> CreateAsync(
            CreateAdmissionRequest request,
            int coachingId)
        {
            if (request.StudentId <= 0)
                throw new ArgumentException("Invalid student.");

            if (request.CourseId <= 0)
                throw new ArgumentException("Invalid course.");

            if (request.AdmissionDate == default)
                throw new ArgumentException(
                    "Admission date is required.");

            if (request.MonthlyFee <= 0)
                throw new ArgumentException(
                    "Monthly fee must be greater than zero.");

            if (request.AdmissionFee < 0)
                throw new ArgumentException(
                    "Admission fee cannot be negative.");

            var admission = new Admission
            {
                CoachingId = coachingId,
                StudentId = request.StudentId,
                CourseId = request.CourseId,
                BatchId = request.BatchId,
                AdmissionDate = request.AdmissionDate,
                MonthlyFee = request.MonthlyFee,
                AdmissionFee = request.AdmissionFee,
                AdmissionFeePaid = request.AdmissionFeePaid,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            return await _admissionRepository.CreateAsync(admission);
        }
    }
}
