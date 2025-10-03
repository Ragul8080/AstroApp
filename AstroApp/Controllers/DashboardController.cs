using Microsoft.AspNetCore.Mvc;

namespace AstroApp.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
