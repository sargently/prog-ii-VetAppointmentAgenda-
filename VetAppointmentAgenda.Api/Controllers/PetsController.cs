using Microsoft.AspNetCore.Mvc;
using VetAppointmentAgenda.Api.Data.Contexto;
using VetAppointmentAgenda.Api.Data.Modelos;
using VetAppointmentAgenda.Api.Data.Entidades;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/pets")]
    public class PetsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PetsController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PetDto>> GetAll()
        {
            var pets = _context.Pets
                .Select(p => new PetDto
                {
                    Id = p.Id,
                    Name = p.Name,
                    Species = p.Species,
                    Breed = p.Breed,
                    BirthDate = p.BirthDate,
                    OwnerId = p.OwnerId,
                    IsActive = p.IsActive
                }).ToList();
            return Ok(pets);
        }

        [HttpGet("{id}")]
        public ActionResult<PetDto> GetById(int id)
        {
            var pet = _context.Pets.Find(id);
            if (pet == null) return NotFound();

            var dto = new PetDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                BirthDate = pet.BirthDate,
                OwnerId = pet.OwnerId,
                IsActive = pet.IsActive
            };
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<PetDto> Create([FromBody] CreatePetDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre de la mascota es requerido.");

            var ownerExists = _context.Owners.Any(o => o.Id == dto.OwnerId);
            if (!ownerExists)
                return BadRequest($"No existe un dueño con Id = {dto.OwnerId}.");

            var pet = new Pet
            {
                Name = dto.Name,
                Species = dto.Species,
                Breed = dto.Breed,
                BirthDate = dto.BirthDate,
                OwnerId = dto.OwnerId,
                IsActive = true
            };

            _context.Pets.Add(pet);
            _context.SaveChanges();

            var result = new PetDto
            {
                Id = pet.Id,
                Name = pet.Name,
                Species = pet.Species,
                Breed = pet.Breed,
                BirthDate = pet.BirthDate,
                OwnerId = pet.OwnerId,
                IsActive = pet.IsActive
            };
            return CreatedAtAction(nameof(GetById), new { id = pet.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdatePetDto dto)
        {
            var pet = _context.Pets.Find(id);
            if (pet == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre de la mascota es requerido.");

            var ownerExists = _context.Owners.Any(o => o.Id == dto.OwnerId);
            if (!ownerExists)
                return BadRequest($"No existe un dueño con Id = {dto.OwnerId}.");

            pet.Name = dto.Name;
            pet.Species = dto.Species;
            pet.Breed = dto.Breed;
            pet.BirthDate = dto.BirthDate;
            pet.OwnerId = dto.OwnerId;
            pet.IsActive = dto.IsActive;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var pet = _context.Pets.Find(id);
            if (pet == null) return NotFound();

            _context.Pets.Remove(pet);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
