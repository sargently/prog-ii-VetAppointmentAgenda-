using Microsoft.AspNetCore.Mvc;
using VetAppointmentAgenda.Api.Data.Contexto;
using VetAppointmentAgenda.Api.Data.Modelos;
using VetAppointmentAgenda.Api.Data.Entidades;


namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/owners")]
    public class OwnersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public OwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<OwnerDto>> GetAll()
        {
            var owners = _context.Owners
                .Select(o => new OwnerDto
                {
                    Id = o.Id,
                    Name = o.Name,
                    Phone = o.Phone,
                    Email = o.Email,
                    IsActive = o.IsActive
                }).ToList();
            return Ok(owners);
        }

        [HttpGet("{id}")]
        public ActionResult<OwnerDto> GetById(int id)
        {
            var owner = _context.Owners.Find(id);
            if (owner == null) return NotFound();

            var dto = new OwnerDto
            {
                Id = owner.Id,
                Name = owner.Name,
                Phone = owner.Phone,
                Email = owner.Email,
                IsActive = owner.IsActive
            };
            return Ok(dto);
        }

        [HttpPost]
        public ActionResult<OwnerDto> Create([FromBody] CreateOwnerDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre del dueño es requerido.");

            var owner = new Owner
            {
                Name = dto.Name,
                Phone = dto.Phone,
                Email = dto.Email,
                IsActive = true
            };

            _context.Owners.Add(owner);
            _context.SaveChanges();

            var result = new OwnerDto
            {
                Id = owner.Id,
                Name = owner.Name,
                Phone = owner.Phone,
                Email = owner.Email,
                IsActive = owner.IsActive
            };
            return CreatedAtAction(nameof(GetById), new { id = owner.Id }, result);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateOwnerDto dto)
        {
            var owner = _context.Owners.Find(id);
            if (owner == null) return NotFound();

            if (string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest("El nombre del dueño es requerido.");

            owner.Name = dto.Name;
            owner.Phone = dto.Phone;
            owner.Email = dto.Email;
            owner.IsActive = dto.IsActive;

            _context.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var owner = _context.Owners.Find(id);
            if (owner == null) return NotFound();

            _context.Owners.Remove(owner);
            _context.SaveChanges();
            return NoContent();
        }
    }
}
