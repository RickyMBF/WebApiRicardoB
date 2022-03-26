using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebApiRicardoB.Validaciones;


namespace WebApiRicardoB.Entities
{
    public class Carro : IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Se requiere introducir el campo {0}.")]
        [StringLength(maximumLength:17, ErrorMessage = "El campo {0} tiene un maximo de 17 caracteres.")]
       
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        //[PrimeraLetraMayuscula]
        public string Brand { get; set; }
        public string Model { get; set; }
        [Range(1800,2025, ErrorMessage = "El valor introducido al campo {0} no esta permitido.")]
        [NotMapped]
        public int Year { get; set; }
        public string Color { get; set; }
        public List<PaisProductor> PaisesProductores { get; set; }
        
        [NotMapped]
        public int Menor { get; set; }

        [NotMapped]
        public int Mayor { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            //Para que se ejecuten debe de primero cumplirse con las reglas por atributo. Ejemplo: Rango
            //Tomar a consideracion que primero se ejecutarán las validaciones mapeadas en los atributos
            //y posteriormente las declaradas en la entidad
            if(!string.IsNullOrEmpty(Brand))
            {
                var primeraLetra = Brand[0].ToString();

                if(primeraLetra != primeraLetra.ToUpper())
                {
                    yield return new ValidationResult("La primera letra debe ser mayúscula",
                        new String[] { nameof(Brand) });
                }
            }

            if(Menor > Mayor)
            {
                yield return new ValidationResult("Este valor no puede ser más grande que el campo Mayor",
                    new String[] {nameof(Menor) });
            }
        }
    }
}
