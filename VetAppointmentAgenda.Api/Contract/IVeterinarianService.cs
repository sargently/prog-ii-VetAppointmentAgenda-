using VetAppointmentAgenda.Api.Dtos.Veterinarian;

namespace VetAppointmentAgenda.Api.Contract
{
    public interface IVeterinarianService
    {
        IEnumerable<VeterinarianServiceDto> GetAll();
        VeterinarianServiceDto? GetById(int id);
        VeterinarianServiceDto Create(CreateVeterinarianServiceDto dto);
        void Update(int id, UpdateVeterinarianServiceDto dto);
        void Delete(int id);
    }
}