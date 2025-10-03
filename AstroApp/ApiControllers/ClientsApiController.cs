using AstroApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AstroApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsApiController : ControllerBase
    {
        private readonly IClientRepository _clientRepo;

        public ClientsApiController(IClientRepository clientRepo)
        {
            _clientRepo = clientRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientRepo.GetAllClientsAsync();
            return Ok(clients);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClient(int id)
        {
            var client = await _clientRepo.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }
    }
}
