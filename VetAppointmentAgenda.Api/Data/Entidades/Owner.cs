namespace VetAppointmentAgenda.Api.Data.Entidades
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsActive { get; set; } = true;

        public List<Pet> Pets { get; set; } = new List<Pet>();
    }
}
