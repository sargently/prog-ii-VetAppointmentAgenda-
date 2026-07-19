namespace VetAppointmentAgenda.Api.Data.Entidades
{
    public class Pet
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public bool IsActive { get; set; } = true;

        public int OwnerId { get; set; }
        public Owner? Owner { get; set; }

        public int? VeterinarianId { get; set; }
        public Veterinarian? Veterinarian { get; set; }
    }
}
