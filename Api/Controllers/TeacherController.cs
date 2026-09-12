using Application.DTOs.Teachers;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherService _teacherService;
        private readonly ICurrentUser _currentUser;

        public TeacherController(
            ITeacherService teacherService,
            ICurrentUser currentUser)
        {
            _teacherService = teacherService;
            _currentUser = currentUser;
        }

        [HttpPost("Create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateTeacherRequest request)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var teacherId =
                await _teacherService.CreateAsync(
                    request,
                    _currentUser.CoachingId.Value);

            return Ok(new
            {
                message = "Teacher created successfully.",
                teacherId
            });
        }

        [HttpGet("GetAllTeacher")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var teachers =
                await _teacherService.GetAllAsync(
                    _currentUser.CoachingId.Value);

            return Ok(teachers);
        }
    }
}
