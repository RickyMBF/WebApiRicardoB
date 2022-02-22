using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRicardoB.Entities;

namespace WebApiRicardoB.Controllers
{
    [ApiController]
    [Route("api/carros")]
    public class CarrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public CarrosController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Carro>>> Get()
        {
            return await dbContext.Carros.Include(x => x.ManufacturingCountries).ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult> Post(Carro carro)
        {
            dbContext.Add(carro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpPut("{id:int}")] //api/carros/1
        public async Task<ActionResult> Put(Carro carro, int id)
        {
            if(carro.Id != id)
            {
                return BadRequest("El ID del carro no coincide con el establecido en la URL.");
            }

            dbContext.Update(carro);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exist = await dbContext.Carros.AnyAsync(x => x.Id == id);
            if(!exist)
            {
                return NotFound();
            }

            dbContext.Remove(new Carro()
            {
                Id = id
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
