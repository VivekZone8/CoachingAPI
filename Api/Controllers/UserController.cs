using Application.DTOs.Users;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("UserCreate")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<IActionResult> Create(CreateUserRequest request)
        {
            var id = await _userService.CreateAsync(request);
            if (id == 0)
            {
                return Ok(new
                {
                    message = "User Already Register",
                    id
                });
            }

            return Ok(new
            {
                message = "User created successfully.",
                id
            });
        }


    }
}
