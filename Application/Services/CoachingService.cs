using Application.DTOs.Coaching;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Services
{
    public class CoachingService:ICoachingService
    {
        private readonly ICoachingRepository _repository;

        public CoachingService(ICoachingRepository repository)
        {
            _repository = repository;
        }

        public async Task<CoachingResponse> AddAsync(AddCoachingRequest request)
        {
            var coaching = new Coaching
            {
                Name = request.Name,
                Code = GenerateCode(request.Name),
                Email = request.Email,
                Phone = request.Phone,
                Address = request.Address,
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            var result = await _repository.AddAsync(coaching);

            return new CoachingResponse
            {
                Id = result.Id,
                Name = result.Name,
                Code = result.Code,
                Email = result.Email,
                Phone = result.Phone,
                Address = result.Address,
                IsActive = result.IsActive,
                CreatedAt = result.CreatedAt,
                Message=result.Message,
                Success=result.Success
            };
        }

        private static string GenerateCode(string name)
        {
            return string.Join("",
                name
                    .Split(' ', StringSplitOptions.RemoveEmptyEntries)
                    .Select(word => char.ToUpper(word[0])));
        }
    }

}
