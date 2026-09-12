using Application.DTOs.Batches;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class BatchService : IBatchService
    {
        private readonly IBatchRepository _batchRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly ITeacherRepository _teacherRepository;
      

        public BatchService(
            IBatchRepository batchRepository,
            ICourseRepository courseRepository,
            ISubjectRepository subjectRepository,
            ITeacherRepository teacherRepository)
        {
            _batchRepository = batchRepository;
            _courseRepository = courseRepository;
            _subjectRepository = subjectRepository;
            _teacherRepository = teacherRepository;
        }

        public async Task<int> CreateAsync(
            CreateBatchRequest request,
            int coachingId)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Batch name is required.");

            if (request.EndDate.HasValue &&
                request.EndDate.Value < request.StartDate)
                throw new ArgumentException(
                    "End date cannot be before start date.");

            if (request.MaxStudents.HasValue &&
                request.MaxStudents <= 0)
                throw new ArgumentException(
                    "Max students must be greater than zero.");

            var courseExists = await _courseRepository.ExistsAsync(
                request.CourseId,
                coachingId);

            if (!courseExists)
                throw new ArgumentException(
                    "Invalid course.");

            if (request.SubjectId.HasValue)
            {
                var subjectExists = await _subjectRepository.ExistsAsync(
                    request.SubjectId.Value,
                    coachingId);

                if (!subjectExists)
                    throw new ArgumentException(
                        "Invalid subject.");
            }

            if (request.TeacherId.HasValue)
            {
                var teacherExists = await _teacherRepository.ExistsAsync(
                    request.TeacherId.Value,
                    coachingId);

                if (!teacherExists)
                    throw new ArgumentException(
                        "Invalid teacher.");
            }

            var batch = new Batch
            {
                CoachingId = coachingId,
                CourseId = request.CourseId,
                SubjectId = request.SubjectId,
                TeacherId = request.TeacherId,
                Name = request.Name.Trim(),
                StartDate = request.StartDate,
                EndDate = request.EndDate,
                MaxStudents = request.MaxStudents,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            return await _batchRepository.CreateAsync(batch);
        }
        public async Task<IEnumerable<Batch>> GetAllAsync(
            int coachingId)
        {
            return await _batchRepository.GetAllAsync(coachingId);
        }
        public async Task<Batch?> GetByIdAsync(
            int id,
            int coachingId)
        {
            return await _batchRepository.GetByIdAsync(
                id,
                coachingId);
        }
    }
}
