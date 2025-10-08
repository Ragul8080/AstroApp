using AstroApp.Models;
using AstroApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AstroApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepo;

        public ClientController(IClientRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }
        public IActionResult Manage()
        {
            return View();
        }

        public ActionResult CreateUser()
        {
            var model = new ClientModel
            {
                Mode = "Create"
            };
            return View("CreateUser", model);
        }

        public async Task<IActionResult> EditUser(int id)
        {
            var client = await _clientRepo.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            client.Mode = "Edit";
            return View("CreateUser", client);
        }
        public async Task<IActionResult> UserDetail(int id)
        {
            var client = await _clientRepo.GetClientByIdAsync(id);
            if (client == null)
            {
                return NotFound();
            }
            return View(client);
        }
    }

}
