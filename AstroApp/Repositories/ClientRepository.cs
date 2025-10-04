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

        public async Task<ClientModel?> GetClientByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"SELECT [ClientId]
                          ,[FirstName]
                          ,[LastName]
                          ,[Email]
                          ,[Phone]
                          ,[Gender]
                          ,[DateOfBirth]
                          ,[CreatedAt]
                          ,[Status]
                          ,[ZodiacSign]
                          ,[BirthTime]
                          ,[AddressLine1]
                          ,[AddressLine2]
                          ,[City]
                          ,[State]
                          ,[ZipCode]
                          ,[ZodiacSignId]
                          ,[StarId]
                          ,[Note]
                      FROM [AstroManager].[dbo].[Clients]  where ClientId = @Id";
            return await connection.QueryFirstOrDefaultAsync<ClientModel>(sql, new { Id = id });
        }

        public async Task<IEnumerable<Sates>> GetAllStatesAsync()
        {
            using var connection = CreateConnection();
            var sql = @"SELECT 
                            [Id] AS SatesId,
                            [State_Name] AS SatesName
                        FROM [AstroManager].[dbo].[State]";
            return await connection.QueryAsync<Sates>(sql);
        }

        public async Task<IEnumerable<ZodiacSign>> GetAllZodiacSignAsync()
        {
            using var connection = CreateConnection();
            var sql = @"SELECT [Zodiac_Id] AS Id,
                               [Zodiac_NameEng] AS EngName,
                               [Zodiac_NameTamil] AS TamilName
                        FROM [AstroManager].[dbo].[ZodiacSign]";
            return await connection.QueryAsync<ZodiacSign>(sql);
        }

        public async Task<IEnumerable<Star>> GetAllStarAsync()
        {
            using var connection = CreateConnection();
            var sql = @"SELECT [Star_Id] AS Id,
                               [Star_NameEng] AS EngName,
                               [Star_NameTamil] AS TamilName
                        FROM [AstroManager].[dbo].[Star]";
            return await connection.QueryAsync<Star>(sql);
        }

        public async Task<bool> CreateClientAsync(ClientModel client)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    string query = @"
            INSERT INTO Clients(
                FirstName, 
                LastName, 
                Email, 
                Phone, 
                Gender, 
                DateOfBirth, 
                BirthTime,
                AddressLine1, 
                AddressLine2, 
                City, 
                State, 
                ZipCode, 
                ZodiacSignId, 
                StarId, 
                Note, 
                CreatedAt, 
                Status)
            VALUES(
                @FirstName, 
                @LastName, 
                @Email, 
                @Phone, 
                @Gender, 
                @DateOfBirth, 
                @BirthTime,
                @AddressLine1,  
                @AddressLine2, 
                @City, 
                @State, 
                @ZipCode,
                @ZodiacSignId, 
                @StarId, 
                @Note, 
                GETDATE(), 
                1)";

                    int rows = await conn.ExecuteAsync(query, client);

                    return rows > 0;
                }
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., to a file, database, or console)
                Console.WriteLine($"Error creating client: {ex.Message}");
                return false;
            }
        }


        public async Task<bool> UpdateClientAsync(ClientModel client)
        {
            using var conn = new SqlConnection(_connectionString);
            string sql = @"
                UPDATE Clients
                SET FirstName = @FirstName,
                    LastName = @LastName,
                    Email = @Email,
                    Phone = @Phone,
                    Gender = @Gender,
                    DateOfBirth = @DateOfBirth,
                    BirthTime = @BirthTime,
                    AddressLine1 = @AddressLine1,
                    AddressLine2 = @AddressLine2,
                    City = @City,
                    State = @State,
                    ZipCode = @ZipCode,
                    ZodiacSignId = @ZodiacSignId,
                    StarId = @StarId,
                    Note = @Note
                WHERE ClientId = @ClientId";
            int rows = await conn.ExecuteAsync(sql, client);
            return rows > 0;
        }
    }
}
