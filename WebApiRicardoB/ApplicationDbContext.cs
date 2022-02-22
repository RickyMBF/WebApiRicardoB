using Microsoft.EntityFrameworkCore;
using WebApiRicardoB.Entities;

namespace WebApiRicardoB
{

    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Carro> Carros { get; set; }
        public DbSet<PaisProductor> ManufacturingCountries { get; set; }
    }
}
