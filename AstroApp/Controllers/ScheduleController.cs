using Microsoft.AspNetCore.Mvc;

namespace AstroApp.Controllers
{
    public class ScheduleController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }
    }
}
