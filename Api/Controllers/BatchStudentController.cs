using Application.DTOs.Batches;
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
    public class BatchStudentController : ControllerBase
    {
        private readonly IBatchStudentService _service;
        private readonly ICurrentUser _currentUser;

        public BatchStudentController(
            IBatchStudentService service,
            ICurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Assign(
            AssignStudentRequest request)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var id = await _service.AssignAsync(
                request,
                _currentUser.CoachingId.Value);

            return Ok(new
            {
                message = "Student assigned to batch successfully.",
                id
            });
        }

        [HttpGet("{batchId:int}")]
        public async Task<IActionResult> GetStudents(int batchId)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var students = await _service.GetStudentsAsync(
                batchId,
                _currentUser.CoachingId.Value);

            return Ok(students);
        }
    }
}
