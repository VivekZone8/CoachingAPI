using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class TimetableController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
