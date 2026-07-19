namespace VetAppointmentAgenda.Api.Data.Modelos
{
    public class PetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }
        public int? VeterinarianId { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreatePetDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }
        public int? VeterinarianId { get; set; }
    }

    public class UpdatePetDto
    {
        public string Name { get; set; } = string.Empty;
        public string Species { get; set; } = string.Empty;
        public string Breed { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public int OwnerId { get; set; }
        public int? VeterinarianId { get; set; }
        public bool IsActive { get; set; }
    }
}
