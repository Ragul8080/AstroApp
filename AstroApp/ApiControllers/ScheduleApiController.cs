using AstroApp.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AstroApp.ApiControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ScheduleApiController : ControllerBase
    {
        private readonly IScheduleRepository _ScheduleRepo;

        public ScheduleApiController(IScheduleRepository ScheduleRepo)
        {
            _ScheduleRepo = ScheduleRepo;
        }

        [HttpGet("GetAppointments")]
        public async Task<IActionResult> GetAppointments()
        {
            try
            {
                var clients = await _ScheduleRepo.GetTodaySchedule();
                return Ok(clients); // ✅ Return JSON array
            }
            catch (Exception ex)
            {
                // Log exception
                Console.Error.WriteLine($"Error fetching clients: {ex.Message}");
                return StatusCode(500, new { message = "Failed to get clients." });
            }
        }

    }
}
