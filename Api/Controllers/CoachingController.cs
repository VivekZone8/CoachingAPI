using Application.DTOs.Coaching;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoachingController : ControllerBase
    {
        private readonly ICoachingService _coachingService;

        public CoachingController(ICoachingService coachingService)
        {
            _coachingService = coachingService;
        }

        [HttpPost("Add")]
        public async Task<IActionResult> Add(AddCoachingRequest request)
        {
            var result = await _coachingService.AddAsync(request);

            if (result.Success == 0)
            {
                return BadRequest(result);
            }

            return Ok(result);
        }

        
    }
}
