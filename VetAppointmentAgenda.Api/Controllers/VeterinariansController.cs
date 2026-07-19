using Microsoft.AspNetCore.Mvc;
using VetAppointmentAgenda.Api.Contract;
using VetAppointmentAgenda.Api.Dtos.Veterinarian;

namespace VetAppointmentAgenda.Api.Controllers
{
    [ApiController]
    [Route("api/veterinarians")]
    public class VeterinariansController : ControllerBase
    {
        private readonly IVeterinarianService _service;

        public VeterinariansController(IVeterinarianService service)
        {
            _service = service;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VeterinarianServiceDto>> GetAll()
        {
            return Ok(_service.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<VeterinarianServiceDto> GetById(int id)
        {
            var vet = _service.GetById(id);
            if (vet == null) return NotFound();
            return Ok(vet);
        }

        [HttpPost]
        public ActionResult<VeterinarianServiceDto> Create([FromBody] CreateVeterinarianServiceDto dto)
        {
            try
            {
                var result = _service.Create(dto);
                return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] UpdateVeterinarianServiceDto dto)
        {
            try
            {
                _service.Update(id, dto);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _service.Delete(id);
                return NoContent();
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}