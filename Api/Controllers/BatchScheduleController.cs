using Application.DTOs.Batches;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BatchScheduleController : ControllerBase
    {
        private readonly IBatchScheduleService _service;
        private readonly ICurrentUser _currentUser;

        public BatchScheduleController(
            IBatchScheduleService service,
            ICurrentUser currentUser)
        {
            _service = service;
            _currentUser = currentUser;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(
            CreateBatchScheduleRequest request)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var id = await _service.CreateAsync(
                request,
                _currentUser.CoachingId.Value);

            return Ok(new
            {
                message = "Batch schedule created successfully.",
                id
            });
        }

        [HttpGet("{batchId:int}")]
        public async Task<IActionResult> GetByBatch(int batchId)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var schedules = await _service.GetByBatchAsync(
                batchId,
                _currentUser.CoachingId.Value);

            return Ok(schedules);
        }
    }
}
