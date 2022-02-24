using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiRicardoB.Entities
{
    public class Carro
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere introducir el campo {0}.")]
        [StringLength(maximumLength:17, ErrorMessage = "El campo {0} tiene un maximo de 17 caracteres.")]
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        [Range(1800,2025, ErrorMessage = "El valor introducido al campo {0} no esta permitido.")]
        [NotMapped]
        public int Year { get; set; }
        public string Color { get; set; }
        public List<PaisProductor> PaisesProductores { get; set; }

    }
}
