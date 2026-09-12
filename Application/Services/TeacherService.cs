using Application.DTOs.Teachers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;

namespace Application.Services
{
       public class TeacherService : ITeacherService
        {
            private readonly ITeacherRepository _teacherRepository;
            private readonly IPasswordHasher _passwordHasher;

            public TeacherService(
                ITeacherRepository teacherRepository,
                IPasswordHasher passwordHasher)
            {
                _teacherRepository = teacherRepository;
                _passwordHasher = passwordHasher;
            }

            public async Task<int> CreateAsync(
                CreateTeacherRequest request,
                int coachingId)
            {
                if (coachingId <= 0)
                    throw new ArgumentException("Invalid coaching.");

                if (string.IsNullOrWhiteSpace(request.Name))
                    throw new ArgumentException("Name is required.");

                if (string.IsNullOrWhiteSpace(request.Email))
                    throw new ArgumentException("Email is required.");

                if (string.IsNullOrWhiteSpace(request.Password))
                    throw new ArgumentException("Password is required.");

                if (request.Password.Length < 8)
                    throw new ArgumentException(
                        "Password must be at least 8 characters.");

                var email = request.Email.Trim().ToLowerInvariant();

                var passwordHash =
                    _passwordHasher.Hash(request.Password);

                var teacher = new Teacher
                {
                    CoachingId = coachingId,
                    Name = request.Name.Trim(),
                    Email=request.Email,
                    Phone = request.Phone?.Trim(),
                    Qualification = request.Qualification?.Trim()
                };

                return await _teacherRepository.CreateAsync(
                    teacher,
                    passwordHash);
            }

            public async Task<IEnumerable<Teacher>> GetAllAsync(int coachingId)
            {
                if (coachingId <= 0)
                    throw new ArgumentException("Invalid coaching.");
                return await _teacherRepository.GetAllAsync(
                    coachingId);
            }
        }
}

