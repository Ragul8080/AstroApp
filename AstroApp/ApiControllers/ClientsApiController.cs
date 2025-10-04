using AstroApp.Models;
using AstroApp.Repositories;
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

        [HttpGet("GetClients")]
        public async Task<IActionResult> GetClients()
        {
            var clients = await _clientRepo.GetAllClientsAsync();
            return Ok(new { data = clients });
        }

        [HttpGet("GetStates")]
        public async Task<IActionResult> GetStates()
        {
            var states = await _clientRepo.GetAllStatesAsync();
            return Ok(new { data = states });
        }

        [HttpGet("GetZodiacSign")]
        public async Task<IActionResult> GetZodiacSign()
        {
            var zodiacSigns = await _clientRepo.GetAllZodiacSignAsync();
            return Ok(new { data = zodiacSigns });
        }

        [HttpGet("GetStar")]
        public async Task<IActionResult> GetStar()
        {
            var star = await _clientRepo.GetAllStarAsync();
            return Ok(new { data = star });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetClientById(int id)
        {
            var client = await _clientRepo.GetClientByIdAsync(id);
            if (client == null) return NotFound();
            return Ok(client);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] ClientModel client)
        {
            if (client == null)
                return BadRequest(new { success = false, message = "Invalid client data." });

            bool result = await _clientRepo.CreateClientAsync(client);

            if (result)
                return Ok(new { success = true, message = "Client created successfully." });
            else
                return StatusCode(500, new { success = false, message = "Failed to create client." });
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ClientModel client)
        {
            if (client == null || id != client.ClientId)
                return BadRequest(new { success = false, message = "Invalid client data or ID mismatch." });

            var existingClient = await _clientRepo.GetClientByIdAsync(id);
            if (existingClient == null)
                return NotFound(new { success = false, message = "Client not found." });

            var result = await _clientRepo.UpdateClientAsync(client);

            if (result)
                return Ok(new { success = true, message = "Client updated successfully." });
            else
                return StatusCode(500, new { success = false, message = "Failed to update client." });
        }
    }

}

