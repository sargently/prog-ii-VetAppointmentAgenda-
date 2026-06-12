using VetAppointmentAgenda.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/veterinarians")]
    public class VeterinariansController : ControllerBase
    {
        private static readonly List<Veterinarian> _veterinarians = new List<Veterinarian>
        {
            new Veterinarian { Id = 1, Name = "Dr. Carlos Mendez", Specialty = "Cirugia", LicenseNumber = "VET-001", IsActive = true },
            new Veterinarian { Id = 2, Name = "Dra. Sofia Reyes", Specialty = "Dermatologia", LicenseNumber = "VET-002", IsActive = true },
            new Veterinarian { Id = 3, Name = "Dr. Luis Torres", Specialty = "Cardiologia", LicenseNumber = "VET-003", IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Veterinarian>> GetAll()
        {
            return Ok(_veterinarians);
        }

        [HttpGet("{id}")]
        public ActionResult<Veterinarian> GetById(int id)
        {
            var vet = _veterinarians.FirstOrDefault(v => v.Id == id);
            if (vet == null)
                return NotFound();
            return Ok(vet);
        }

        [HttpPost]
        public ActionResult<Veterinarian> Create(Veterinarian vet)
        {
            if (string.IsNullOrWhiteSpace(vet.Name))
                return BadRequest("El nombre del veterinario es requerido.");

            if (string.IsNullOrWhiteSpace(vet.LicenseNumber))
                return BadRequest("El numero de licencia es requerido.");

            int newId = _veterinarians.Any() ? _veterinarians.Max(v => v.Id) + 1 : 1;
            vet.Id = newId;
            vet.IsActive = true;
            _veterinarians.Add(vet);

            return CreatedAtAction(nameof(GetById), new { id = vet.Id }, vet);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Veterinarian vet)
        {
            var existing = _veterinarians.FirstOrDefault(v => v.Id == id);
            if (existing == null)
                return NotFound();

            existing.Name = vet.Name;
            existing.Specialty = vet.Specialty;
            existing.LicenseNumber = vet.LicenseNumber;
            existing.IsActive = vet.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _veterinarians.FirstOrDefault(v => v.Id == id);
            if (existing == null)
                return NotFound();

            _veterinarians.Remove(existing);
            return NoContent();
        }
    }
}