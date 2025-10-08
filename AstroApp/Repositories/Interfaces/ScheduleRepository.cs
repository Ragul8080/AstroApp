using System.Data;
using System.Data.SqlClient;
using AstroApp.Models;
using Dapper;

namespace AstroApp.Repositories.Interfaces
{
    public class ScheduleRepository : IScheduleRepository
    {
        private readonly string _connectionString;

        public ScheduleRepository(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        private IDbConnection CreateConnection()
            => new SqlConnection(_connectionString);
        public async Task<IEnumerable<ClientModel>> GetTodaySchedule()
        {
            try
            {
                using var connection = CreateConnection();
                var sql = @"
                            SELECT 
                            c.[ClientId],
                            c.[FirstName],
                            c.[LastName],
                            c.[Email],
                            c.[Phone],
                            c.[Gender],
                            c.[DateOfBirth],
                            c.[CreatedAt],
                            c.[Status],
                            c.[ZodiacSign],
                            CONVERT(varchar(8), c.[BirthTime], 108) AS BirthTime,  -- convert TIME to HH:mm:ss
                            c.[AddressLine1],
                            c.[AddressLine2],
                            c.[City],
                            c.[State],
                            c.[ZipCode],
                            c.[ZodiacSignId],
                            c.[StarId],
                            c.[Note],
                            a.[AppointmentId],
                            a.[AppointmentDate],
                            a.[SessionMode],
                            a.[TimeSlot]
                        FROM [AstroManager].[dbo].[Clients] c
                        INNER JOIN [AstroManager].[dbo].[Appointments] a
                            ON c.ClientId = a.ClientId
                        WHERE a.AppointmentDate = CAST(GETDATE() AS DATE)
                        ORDER BY a.AppointmentDate, c.FirstName;
                        ";

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
    }
}
