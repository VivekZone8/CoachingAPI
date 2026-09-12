using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System.Text.RegularExpressions;

namespace Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimetableController : ControllerBase
    {
        [HttpGet("Index")]
        public IActionResult Index(String num)
        {
           
            string pattern = @"^[a-zA-Z0-9]+@[a-zA-Z0-9]+\.[a-zA-Z]{2,}$";
            Match match = Regex.Match(num, pattern);
            if(match.Success)
            {
                return Ok(new
                {
                    message = "Valid email address"
                });
            }
            else
            {
                return BadRequest(new
                {
                    message = "Invalid email address"
                });
            }
        }
    }
}
