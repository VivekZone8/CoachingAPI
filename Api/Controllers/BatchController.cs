using Application.DTOs.Batches;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class BatchController : Controller
    {

        private readonly IBatchService _batchService;

        public BatchController(IBatchService batchService)
        {
            _batchService = batchService;
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(CreateBatchRequest request)
        {
            var coachingIdClaim = User.FindFirst("CoachingId")?.Value;

            if (!int.TryParse(coachingIdClaim, out var coachingId))
                return Forbid();

            var batchId = await _batchService.CreateAsync(request, coachingId);

            return Ok(new
            {
                message = "Batch created successfully.",
                id = batchId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var coachingIdClaim = User.FindFirst("CoachingId")?.Value;

            if (!int.TryParse(coachingIdClaim, out var coachingId))
                return Forbid();

            var batches = await _batchService.GetAllAsync(coachingId);

            return Ok(batches);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var coachingIdClaim = User.FindFirst("CoachingId")?.Value;

            if (!int.TryParse(coachingIdClaim, out var coachingId))
                return Forbid();

            var batch = await _batchService.GetByIdAsync(id, coachingId);

            if (batch == null)
                return NotFound(new
                {
                    message = "Batch not found."
                });

            return Ok(batch);
        }
    }
}
