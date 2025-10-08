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

        public string ZodiacEng { get; set; }
        public string ZodiacTamil { get; set; }
        public string StarEng { get; set; }
        public string StarTamil { get; set; }
        public bool appointmentChk { get; set; }
        public string Mode { get; set; } = "Create";

        public List<AppointmentModel> Appointments { get; set; }
    }
    public class AppointmentModel
    {
        public DateTime AppointmentDate { get; set; }
        public string SessionMode { get; set; }
        public string TimeSlot { get; set; }
    }
}
