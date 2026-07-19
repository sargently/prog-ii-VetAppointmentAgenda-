using Microsoft.AspNetCore.Mvc;
using VetAppointmentAgenda.Api.Data.Entidades;
using VetAppointmentAgenda.Api.Data.Modelos;
using VetAppointmentAgenda.Api.Data.Repositorio;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/veterinarians")]
    public class VeterinariansController : ControllerBase
    {
        private readonly IVeterinarianRepository _repository;

        public VeterinariansController(IVeterinarianRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VeterinarianDto>> GetAll()
        {
            var vets = _repository.GetAll().Select(v => new VeterinarianDto
            {
                Id = v.Id,
                Name = v.Name,
                Specialty = v.Specialty,
                LicenseNumber = v.LicenseNumber,
                IsActive = v.IsActive
            });
            return Ok(vets);
        }

        [HttpGet("{id}")]
        public ActionResult<VeterinarianDto> GetById(int id)
        {
            var vet = _repository.GetById(id);
            if (vet == null) return NotFound();

            return Ok(new VeterinarianDto
            {
                Id = vet.Id,
                Name = vet.Name,
                Specialty = vet.Specialty,
                LicenseNumber = vet.LicenseNumber,
                IsActive = vet.IsActive
            });
        }

        [HttpPost]
        public ActionResult<VeterinarianDto> Create([FromBody] CreateVeterinarianDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre del veterinario es requerido.");

            if (string.IsNullOrWhiteSpace(dto.LicenseNumber))
                return BadRequest("El numero de licencia es requerido.");

            var vet = new Veterinarian
            {
                Name = dto.Name,
                Specialty = dto.Specialty,
                LicenseNumber = dto.LicenseNumber,
                IsActive = true
            };

            _repository.Add(vet);
            _repository.SaveChanges();

            var result = new VeterinarianDto
            {
                Id = vet.Id,
                Name = vet.Name,
                Specialty = vet.Specialty,
                LicenseNumber = vet.LicenseNumber,
                IsActive = vet.IsActive
            };
            return CreatedAtAction(nameof(GetById), new { id = vet.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateVeterinarianDto dto)
        {
            var vet = _repository.GetById(id);
            if (vet == null) return NotFound();

            vet.Name = dto.Name;
            vet.Specialty = dto.Specialty;
            vet.LicenseNumber = dto.LicenseNumber;
            vet.IsActive = dto.IsActive;

            _repository.Update(vet);
            _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var vet = _repository.GetById(id);
            if (vet == null) return NotFound();

            _repository.Delete(vet);
            _repository.SaveChanges();
            return NoContent();
        }
    }
}
