using Application.DTOs.Users;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
        }

        public async Task<int> CreateAsync(CreateUserRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Name))
                throw new ArgumentException("Name is required.");

            if (string.IsNullOrWhiteSpace(request.Email))
                throw new ArgumentException("Email is required.");

            if (string.IsNullOrWhiteSpace(request.Password))
                throw new ArgumentException("Password is required.");
            if (await _userRepository.CheckCoachingExistsAsync((int)request.CoachingId))
                throw new ArgumentException("Coaching Not foud");

            if (await _userRepository.EmailExistsAsync(request.Email))
                throw new ArgumentException("Email already exists.");

            var user = new User
            {
                Name = request.Name.Trim(),
                Email = request.Email.Trim().ToLower(),
                PasswordHash = _passwordHasher.Hash(request.Password),
                Role = UserRole.Admin,
                CoachingId = request.CoachingId,
                IsActive = true
            };

            return await _userRepository.CreateAsync(user);
        }
    }
}
