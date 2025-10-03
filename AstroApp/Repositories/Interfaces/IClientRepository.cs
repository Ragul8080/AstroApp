using AstroApp.Models;

namespace AstroApp.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<IEnumerable<Client>> GetAllClientsAsync();
        public Task<Client?> GetClientByIdAsync(int id);
    }
}
