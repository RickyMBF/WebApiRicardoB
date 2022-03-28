using WebApiRicardoB.Validaciones;

namespace WebApiRicardoB.Entities
{
    public class PaisProductor
    {
        public int Id { get; set; }
        [PrimeraLetraMayuscula]
        public string CountryName { get; set;}
        public int NumberOfFactories { get; set; }
    }
}
