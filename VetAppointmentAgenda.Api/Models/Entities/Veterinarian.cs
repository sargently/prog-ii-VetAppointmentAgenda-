namespace VetAppointmentAgenda.Api.Models.Entities
{
    public class Veterinarian
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;
    }
}