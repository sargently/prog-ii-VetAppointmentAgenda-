using VetAppointmentAgenda.Api.Contract;
using VetAppointmentAgenda.Api.Data.Entidades;
using VetAppointmentAgenda.Api.Data.Repositorio;
using VetAppointmentAgenda.Api.Dtos.Veterinarian;

namespace VetAppointmentAgenda.Api.Service
{
    public class VeterinarianService : IVeterinarianService
    {
        private readonly IVeterinarianRepository _repository;

        public VeterinarianService(IVeterinarianRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<VeterinarianServiceDto> GetAll()
        {
            return _repository.GetAll().Select(MapToDto);
        }

        public VeterinarianServiceDto? GetById(int id)
        {
            var vet = _repository.GetById(id);
            return vet == null ? null : MapToDto(vet);
        }

        public VeterinarianServiceDto Create(CreateVeterinarianServiceDto dto)
        {
            ValidateName(dto.Name);
            ValidateSpecialty(dto.Specialty);
            ValidateLicenseNumber(dto.LicenseNumber);

            var vet = new Veterinarian
            {
                Name = dto.Name.Trim(),
                Specialty = dto.Specialty.Trim(),
                LicenseNumber = dto.LicenseNumber.Trim(),
                IsActive = true
            };

            _repository.Add(vet);
            _repository.SaveChanges();

            return MapToDto(vet);
        }

        public void Update(int id, UpdateVeterinarianServiceDto dto)
        {
            var vet = _repository.GetById(id)
                ?? throw new KeyNotFoundException($"No existe un veterinario con Id = {id}.");

            ValidateName(dto.Name);
            ValidateSpecialty(dto.Specialty);
            ValidateLicenseNumber(dto.LicenseNumber);

            vet.Name = dto.Name.Trim();
            vet.Specialty = dto.Specialty.Trim();
            vet.LicenseNumber = dto.LicenseNumber.Trim();
            vet.IsActive = dto.IsActive;

            _repository.Update(vet);
            _repository.SaveChanges();
        }

        public void Delete(int id)
        {
            var vet = _repository.GetById(id)
                ?? throw new KeyNotFoundException($"No existe un veterinario con Id = {id}.");

            _repository.Delete(vet);
            _repository.SaveChanges();
        }

        private static VeterinarianServiceDto MapToDto(Veterinarian vet)
        {
            return new VeterinarianServiceDto
            {
                Id = vet.Id,
                Name = vet.Name,
                Specialty = vet.Specialty,
                LicenseNumber = vet.LicenseNumber,
                IsActive = vet.IsActive
            };
        }

        private static void ValidateName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("El nombre es requerido.");
            if (name.Trim().Length < 3 || name.Trim().Length > 100)
                throw new ArgumentException("El nombre debe tener entre 3 y 100 caracteres.");
        }

        private static void ValidateSpecialty(string specialty)
        {
            if (string.IsNullOrWhiteSpace(specialty))
                throw new ArgumentException("La especialidad es requerida.");
            if (specialty.Trim().Length < 3 || specialty.Trim().Length > 80)
                throw new ArgumentException("La especialidad debe tener entre 3 y 80 caracteres.");
        }

        private static void ValidateLicenseNumber(string licenseNumber)
        {
            if (string.IsNullOrWhiteSpace(licenseNumber))
                throw new ArgumentException("El numero de licencia es requerido.");
            if (!System.Text.RegularExpressions.Regex.IsMatch(licenseNumber.Trim(), @"^VET-\d{3,}$"))
                throw new ArgumentException("La licencia debe tener el formato VET-000.");
        }
    }
}