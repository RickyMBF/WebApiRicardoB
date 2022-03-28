using System.ComponentModel.DataAnnotations;
using WebApiRicardoB.Validaciones;

namespace WebApiRicardoB.DTOs
{
    public class CarroDTO
    {
        [Required(ErrorMessage = "El campo {0} es requerido")] //
        [StringLength(maximumLength: 17, ErrorMessage = "El campo {0} solo puede tener hasta 17 caracteres")]
        public string VIN { get; set; }
    }
}
