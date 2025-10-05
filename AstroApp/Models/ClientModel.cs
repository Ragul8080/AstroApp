namespace AstroApp.Models
{
    public class ClientModel
    {
        public int? ClientId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public string? BirthTime { get; set; }

        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
        public int? ZodiacSignId { get; set; }
        public int? StarId { get; set; }
        public string Note { get; set; }

        public string Mode { get; set; } = "Create";
    }
}
