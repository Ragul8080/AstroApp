using AstroApp.Models;

namespace AstroApp.Repositories.Interfaces
{
    public interface IClientRepository
    {
        public Task<bool> CreateClientAsync(ClientModel client);

        public Task<bool> UpdateClientAsync(ClientModel client);

        public Task<IEnumerable<Client>> GetAllClientsAsync();

        public Task<IEnumerable<Sates>> GetAllStatesAsync();

        public Task<IEnumerable<ZodiacSign>> GetAllZodiacSignAsync();

        public Task<IEnumerable<Star>> GetAllStarAsync();

        public Task<ClientModel?> GetClientByIdAsync(int id);
    }
}
