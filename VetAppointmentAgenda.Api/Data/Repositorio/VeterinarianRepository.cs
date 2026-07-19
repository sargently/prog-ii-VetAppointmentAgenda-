using Microsoft.EntityFrameworkCore;
using VetAppointmentAgenda.Api.Data.Contexto;
using VetAppointmentAgenda.Api.Data.Entidades;

namespace VetAppointmentAgenda.Api.Data.Repositorio
{
    public class VeterinarianRepository : Repository<Veterinarian>, IVeterinarianRepository
    {
        public VeterinarianRepository(ApplicationDbContext context) : base(context) { }

        public IEnumerable<Veterinarian> GetBySpecialty(string specialty)
        {
            return _dbSet.Where(v => v.Specialty.Contains(specialty)).ToList();
        }
    }
}
