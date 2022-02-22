namespace WebApiRicardoB.Entities
{
    public class Carro
    {
        public int Id { get; set; }
        public string VIN { get; set; }
        public string LicensePlate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public List<PaisProductor> ManufacturingCountries { get; set; }

    }
}
