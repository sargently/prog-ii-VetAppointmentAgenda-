using VetAppointmentAgenda.Api.Data.Entidades;

namespace VetAppointmentAgenda.Api.Data.Repositorio
{
    public interface IVeterinarianRepository : IRepository<Veterinarian>
    {
        IEnumerable<Veterinarian> GetBySpecialty(string specialty);
    }
}
