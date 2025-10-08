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

        public async Task<IEnumerable<ClientModel>> GetAllClientsAsync()
        {
            try
            {
                using var connection = CreateConnection();
                var sql = @"
                            SELECT [ClientId],
                                   [FirstName],
                                   [LastName],
                                   [Email],
                                   [Phone],
                                   [Gender],
                                   [DateOfBirth],
                                   [CreatedAt],
                                   [Status],
                                   [ZodiacSign],
                                   CONVERT(varchar(8), [BirthTime], 108) AS BirthTime,  -- convert TIME to string HH:mm:ss
                                   [AddressLine1],
                                   [AddressLine2],
                                   [City],
                                   [State],
                                   [ZipCode],
                                   [ZodiacSignId],
                                   [StarId],
                                   [Note]
                            FROM [AstroManager].[dbo].[Clients]";

                return await connection.QueryAsync<ClientModel>(sql);
            }
            catch (Exception ex)
            {
                // Log the exception (use your preferred logging method)
                Console.Error.WriteLine($"Error fetching clients: {ex.Message}");
                // Optionally, rethrow or return an empty list
                return Enumerable.Empty<ClientModel>();
            }
        }


        public async Task<ClientModel?> GetClientByIdAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"SELECT [ClientId],
                                   [FirstName],
                                   [LastName],
                                   [Email],
                                   [Phone],
                                   [Gender],
                                   [DateOfBirth],
                                   [CreatedAt],
                                   [Status],
                                   [ZodiacSign],
                                   CONVERT(varchar(8), [BirthTime], 108) AS BirthTime,  -- convert TIME to string HH:mm:ss
                                   [AddressLine1],
                                   [AddressLine2],
                                   [City],
                                   [State],
                                   [ZipCode],
                                   [ZodiacSignId],
                                   [StarId],
                                   [Note],
                                   Z.Zodiac_NameEng AS ZodiacEng,
								   Z.Zodiac_NameTamil AS ZodiacTamil,
								   S.Star_NameEng AS StarEng,
								   S.Star_NameTamil AS StarTamil 
                            FROM [AstroManager].[dbo].[Clients] AS C
							LEFT JOIN ZodiacSign AS Z
								ON Z.Zodiac_Id = C.ZodiacSignId
							LEFT JOIN Star AS S
							    ON S.Star_Id = C.StarId
							WHERE C.ClientId = @Id";
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
                    await conn.OpenAsync();
                    using (var tran = conn.BeginTransaction())
                    {
                        string clientQuery = @"
                            INSERT INTO Clients(
                                FirstName, LastName, Email, Phone, Gender, DateOfBirth, BirthTime,
                                AddressLine1, AddressLine2, City, State, ZipCode, ZodiacSignId, StarId, Note, CreatedAt, Status,IsAppointment)
                            VALUES(
                                @FirstName, @LastName, @Email, @Phone, @Gender, @DateOfBirth, @BirthTime,
                                @AddressLine1, @AddressLine2, @City, @State, @ZipCode, @ZodiacSignId, @StarId, @Note, GETDATE(), 1, @appointmentChk);
                            SELECT CAST(SCOPE_IDENTITY() as int)";

                        int clientId = await conn.QuerySingleAsync<int>(clientQuery, client, tran);

                        if (client.Appointments != null && client.Appointments.Count > 0)
                        {
                            string apptQuery = @"
                            INSERT INTO Appointments(ClientId, AppointmentDate, SessionMode, TimeSlot, CreatedAt, Status)
                            VALUES(@ClientId, @AppointmentDate, @SessionMode, @TimeSlot, GETDATE(), 1)";

                            foreach (var appt in client.Appointments)
                            {
                                await conn.ExecuteAsync(apptQuery, new
                                {
                                    ClientId = clientId,
                                    AppointmentDate = appt.AppointmentDate,
                                    SessionMode = appt.SessionMode,
                                    TimeSlot = appt.TimeSlot
                                }, tran);
                            }
                        }

                        tran.Commit();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
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

        public async Task<IEnumerable<Gender>> GetAllGenderAsync()
        {
            using var connection = CreateConnection();
            var sql = @"SELECT 
	                    [Gender_Id] AS GenderId,
                        [Gender_Name] AS GenderName
                      FROM [AstroManager].[dbo].[Gender]";
            return await connection.QueryAsync<Gender>(sql);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            using var connection = CreateConnection();
            var sql = @"DELETE FROM [AstroManager].[dbo].[Clients] WHERE ClientId = @ClientId";

            var rowsAffected = await connection.ExecuteAsync(sql, new { ClientId = id });
            return rowsAffected > 0;
        }
    }
}
