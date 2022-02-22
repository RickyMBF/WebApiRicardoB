using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRicardoB.Entities;

namespace WebApiRicardoB.Controllers
{
    [ApiController]
    [Route("api/manufacturingcountries")]

    public class ManufacturingCountriesController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public ManufacturingCountriesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<PaisProductor>>> GetAll()
        {
            return await dbContext.ManufacturingCountries.ToListAsync();
        }

        [HttpGet("id:int")]
        public async Task<ActionResult<PaisProductor>> GetById(int id)
        {
            return await dbContext.ManufacturingCountries.FirstOrDefaultAsync(x => x.Id == id);
        }

        [HttpPost]
        public async Task<ActionResult> Post(PaisProductor paisProductor)
        {
            var existeCarro = await dbContext.Carros.AnyAsync(x => x.Id == paisProductor.CarroId);

            if (!existeCarro)
            {
                return BadRequest($"No existe el carro con el ID: {paisProductor.CarroId} ");
            }

            dbContext.Add(paisProductor);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(PaisProductor paisProductor, int id)
        {
            var exist = await dbContext.ManufacturingCountries.AnyAsync(x => x.Id == id);

            if (!exist)
            {
                return NotFound("El pais productor especificado no existe. ");
            }

            if (paisProductor.Id != id)
            {
                return BadRequest("El id del pais productor no coincide con el establecido en la URL. ");
            }

            dbContext.Update(paisProductor);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.ManufacturingCountries.AnyAsync(x => x.Id == id);
            if (!exist)
            {
                return NotFound("El recurso no fue encontrado. ");
            }

            //var validateRelation = await dbContext.CarropaisProductor.AnyAsync (?)

            dbContext.Remove(new PaisProductor { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }
}
