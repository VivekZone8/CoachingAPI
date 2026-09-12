using Application.DTOs.Batches;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BatchStudentService : IBatchStudentService
    {
        private readonly IBatchStudentRepository _repository;

        public BatchStudentService(
            IBatchStudentRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AssignAsync(
            AssignStudentRequest request,
            int coachingId)
        {
            if (request.BatchId <= 0)
                throw new ArgumentException("Invalid batch.");

            if (request.StudentId <= 0)
                throw new ArgumentException("Invalid student.");

            if (request.JoiningDate == default)
                throw new ArgumentException("Joining date is required.");

            var alreadyExists = await _repository.ExistsAsync(
                request.BatchId,
                request.StudentId,
                coachingId);

            if (alreadyExists)
                throw new ArgumentException(
                    "Student is already assigned to this batch.");

            var batchStudent = new BatchStudent
            {
                CoachingId = coachingId,
                BatchId = request.BatchId,
                StudentId = request.StudentId,
                JoiningDate = request.JoiningDate,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            return await _repository.AssignAsync(batchStudent);
        }

        public async Task<IEnumerable<BatchStudent>> GetStudentsAsync(
            int batchId,
            int coachingId)
        {
            if (batchId <= 0)
                throw new ArgumentException("Invalid batch.");

            return await _repository.GetStudentsAsync(
                batchId,
                coachingId);
        }
    }
}
