namespace WebApiRicardoB.Entities
{
    public class PaisProductor
    {
        public int Id { get; set; }
        public string CountryName { get; set;}
        public int NumberOfFactories { get; set; }
        public int CarroId { get; set; }
        public Carro Carro { get; set; }

    }
}
