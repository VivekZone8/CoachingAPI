using Application.DTOs.Enrollment;
using Application.DTOs.Students;
using Application.Interfaces.Repositories;
using Application.Interfaces.Services;
using Application.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentService _studentService;
        private readonly IEnrollmentService _enrollmentService;
        public StudentController(
            IStudentService studentService, IEnrollmentService enrollmentService)
        {
            _studentService = studentService;
            _enrollmentService = enrollmentService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(
            RegisterStudentRequest request)
        {
            var result =
                await _studentService.RegisterAsync(request);

            return Ok(result);
        }

        [HttpPost("EnrollStudent")]
        public async Task<IActionResult> EnrollStudent(
           EnrollStudentRequest request)
        {
            var result =
                await _enrollmentService.EnrollStudentAsync(request);

            return Ok(result);
        }
    }
}
