using Application.DTOs.Admission;
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
    public class AdmissionController : ControllerBase
    {
        private readonly IAdmissionService _admissionService;
        private readonly ICurrentUser _currentUser;

        public AdmissionController(
            IAdmissionService admissionService,
            ICurrentUser currentUser)
        {
            _admissionService = admissionService;
            _currentUser = currentUser;
        }

        [HttpPost("Create")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> Create(
            CreateAdmissionRequest request)
        {
            if (!_currentUser.CoachingId.HasValue)
                return Forbid();

            var admissionId = await _admissionService.CreateAsync(
                request,
                _currentUser.CoachingId.Value);

            return Ok(new
            {
                message = "Admission created successfully.",
                id = admissionId
            });
        }
    }
}
