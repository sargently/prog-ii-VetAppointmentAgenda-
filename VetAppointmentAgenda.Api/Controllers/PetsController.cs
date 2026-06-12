using VetAppointmentAgenda.Api.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {
        private static readonly List<Pet> _pets = new List<Pet>
        {
            new Pet { Id = 1, Name = "Luna", Species = "Perro", Breed = "Labrador", BirthDate = new DateTime(2020, 3, 15), OwnerId = 1, IsActive = true },
            new Pet { Id = 2, Name = "Michi", Species = "Gato", Breed = "Siames", BirthDate = new DateTime(2021, 6, 10), OwnerId = 2, IsActive = true },
            new Pet { Id = 3, Name = "Rocky", Species = "Perro", Breed = "Bulldog", BirthDate = new DateTime(2019, 11, 20), OwnerId = 1, IsActive = true }
        };

        [HttpGet]
        public ActionResult<IEnumerable<Pet>> GetAll()
        {
            return Ok(_pets);
        }

        [HttpGet("{id}")]
        public ActionResult<Pet> GetById(int id)
        {
            var pet = _pets.FirstOrDefault(p => p.Id == id);
            if (pet == null)
                return NotFound();
            return Ok(pet);
        }

        [HttpPost]
        public ActionResult<Pet> Create(Pet pet)
        {
            if (string.IsNullOrWhiteSpace(pet.Name))
                return BadRequest("El nombre de la mascota es requerido.");

            if (pet.OwnerId <= 0)
                return BadRequest("El OwnerId es requerido.");

            int newId = _pets.Any() ? _pets.Max(p => p.Id) + 1 : 1;
            pet.Id = newId;
            pet.IsActive = true;
            _pets.Add(pet);

            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, pet);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, Pet pet)
        {
            var existing = _pets.FirstOrDefault(p => p.Id == id);
            if (existing == null)
                return NotFound();

            existing.Name = pet.Name;
            existing.Species = pet.Species;
            existing.Breed = pet.Breed;
            existing.BirthDate = pet.BirthDate;
            existing.OwnerId = pet.OwnerId;
            existing.IsActive = pet.IsActive;

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var existing = _pets.FirstOrDefault(p => p.Id == id);
            if (existing == null)
                return NotFound();

            _pets.Remove(existing);
            return NoContent();
        }
    }
}