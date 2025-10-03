using AstroApp.Models;
using AstroApp.Repositories.Interfaces;
using Dapper;
using System.Data;
using System.Data.SqlClient;
namespace AstroApp.Repositories

{
    public class ClientRepository : IClientRepository
    {
        private readonly string _connectionString;

        public ClientRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            using var connection = CreateConnection();
            var sql = @"SELECT [ClientId],
                   [FirstName],
                   [LastName],
                   [Email],
                   [Phone],
                   [Gender],
                   [DateOfBirth] AS BirthDate,
                   [CreatedAt],
                   [Status],
                   [ZodiacSign]
            FROM [AstroManager].[dbo].[Clients]";
            return await connection.QueryAsync<Client>(sql);
        }

        public async Task<Client?> GetClientByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"SELECT Id, FullName, ZodiacSign, BirthDate, Email, Phone, LastVisit 
                        FROM Clients WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Client>(sql, new { Id = id });
        }
    }
}
