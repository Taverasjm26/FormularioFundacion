using System.ComponentModel.DataAnnotations;

namespace Formulario.Models
{
    public class SolicitudInput
    {
        [Required(ErrorMessage = "El nombre es obligatorio")]
        [StringLength(80, MinimumLength = 3, ErrorMessage = "El nombre debe tener entre 3 y 80 caracteres")]
        public string Nombre { get; set; } = "";

        [Required(ErrorMessage = "El teléfono es obligatorio")]
        [Phone(ErrorMessage = "Teléfono inválido")]
        [StringLength(20, ErrorMessage = "El teléfono es demasiado largo")]
        public string Telefono { get; set; } = "";

        [Required(ErrorMessage = "La cédula es obligatoria")]
        [StringLength(20, MinimumLength = 5, ErrorMessage = "Cédula inválida")]
        public string Cedula { get; set; } = "";

        [Required(ErrorMessage = "El país es obligatorio")]
        [StringLength(60, ErrorMessage = "País inválido")]
        public string Pais { get; set; } = "";

        [Required(ErrorMessage = "La cantidad es obligatoria")]
        [Range(0.01, 999999999, ErrorMessage = "La cantidad debe ser mayor que 0")]
        public decimal Cantidad { get; set; }

        [Required(ErrorMessage = "El propósito es obligatorio")]
        [StringLength(200, MinimumLength = 3, ErrorMessage = "El propósito debe tener entre 3 y 200 caracteres")]
        public string Proposito { get; set; } = "";
    }
}
