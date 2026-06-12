using VetAppointmentAgenda.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnersController : ControllerBase
    {
        private static readonly List<Owner> _owners = new List<Owner>
        {
            new Owner { Id = 1, Name = "Maria Garcia", Phone = "809-555-0001", Email = "maria@email.com", IsActive = true },
            new Owner { Id = 2, Name = "Juan Perez", Phone = "809-555-0002", Email = "juan@email.com", IsActive = true },
            new Owner { Id = 3, Name = "Ana Lopez", Phone = "809-555-0003", Email = "ana@email.com", IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Owner>> GetAll()
        {
            return Ok(_owners);
        }

        [HttpGet("{id}")]
        public ActionResult<Owner> GetById(int id)
        {
            var owner = _owners.FirstOrDefault(o => o.Id == id);
            if (owner == null)
                return NotFound();
            return Ok(owner);
        }

        [HttpPost]
        public ActionResult<Owner> Create(Owner owner)
        {
            if (string.IsNullOrWhiteSpace(owner.Name))
                return BadRequest("El nombre del dueño es requerido.");

            int newId = _owners.Any() ? _owners.Max(o => o.Id) + 1 : 1;
            owner.Id = newId;
            owner.IsActive = true;
            _owners.Add(owner);

            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, owner);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Owner owner)
        {
            var existing = _owners.FirstOrDefault(o => o.Id == id);
            if (existing == null)
                return NotFound();

            existing.Name = owner.Name;
            existing.Phone = owner.Phone;
            existing.Email = owner.Email;
            existing.IsActive = owner.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _owners.FirstOrDefault(o => o.Id == id);
            if (existing == null)
                return NotFound();

            _owners.Remove(existing);
            return NoContent();
        }
    }
}