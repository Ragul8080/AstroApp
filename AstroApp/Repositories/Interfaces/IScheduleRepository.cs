using AstroApp.Models;

namespace AstroApp.Repositories.Interfaces
{
    public interface IScheduleRepository
    {
        public Task<IEnumerable<ClientModel>> GetTodaySchedule();
    }
}
