using Microsoft.AspNetCore.Mvc;

namespace AstroApp.Controllers
{
    public class ClientController : Controller
    {
        public IActionResult Manage()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            return PartialView("_CreateUser");
        }
    }

}
