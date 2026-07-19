namespace VetAppointmentAgenda.Api.Data.Entidades
{
    public class Veterinarian
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
