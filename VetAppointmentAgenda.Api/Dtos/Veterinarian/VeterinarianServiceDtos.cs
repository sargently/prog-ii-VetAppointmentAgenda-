using System.ComponentModel.DataAnnotations;

namespace VetAppointmentAgenda.Api.Dtos.Veterinarian
{
    public class VeterinarianServiceDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Specialty { get; set; } = string.Empty;
        public string LicenseNumber { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }

    public class CreateVeterinarianServiceDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es requerida.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "La especialidad debe tener entre 3 y 80 caracteres.")]
        public string Specialty { get; set; } = string.Empty;

        [Required(ErrorMessage = "El numero de licencia es requerido.")]
        [RegularExpression(@"^VET-\d{3,}$", ErrorMessage = "La licencia debe tener el formato VET-000.")]
        public string LicenseNumber { get; set; } = string.Empty;
    }

    public class UpdateVeterinarianServiceDto
    {
        [Required(ErrorMessage = "El nombre es requerido.")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 100 caracteres.")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "La especialidad es requerida.")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "La especialidad debe tener entre 3 y 80 caracteres.")]
        public string Specialty { get; set; } = string.Empty;

        [Required(ErrorMessage = "El numero de licencia es requerido.")]
        [RegularExpression(@"^VET-\d{3,}$", ErrorMessage = "La licencia debe tener el formato VET-000.")]
        public string LicenseNumber { get; set; } = string.Empty;

        public bool IsActive { get; set; }
    }
}