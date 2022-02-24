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

        [HttpGet] //api/carro
        [HttpGet("listado")] //api/carros/listado
        [HttpGet("/listado")] // /listado
        public async Task<ActionResult<List<Carro>>> Get()
        {
            return await dbContext.Carros.Include(x => x.PaisesProductores).ToListAsync();
        }

        [HttpGet("primero")] //api/carros/primero
        public async Task<ActionResult<Carro>> PrimerCarro([FromHeader] int valor, [FromQuery] string carro, [FromQuery] int carroId)
        {
            return await dbContext.Carros.FirstOrDefaultAsync();
        }

        [HttpGet("primero2")] //api/carros/primero
        public ActionResult<Carro> PrimerCarroD()
        {
            return new Carro() { VIN = "DOS" };
        }

        [HttpGet("{id:int}/{param?}")]
        public async Task<ActionResult<Carro>> Get(int id, string param)
        {
            var carro = await dbContext.Carros.FirstOrDefaultAsync(x => x.Id == id);

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        [HttpGet("{VIN}")]
        public async Task<ActionResult<Carro>> Get([FromRoute] string VIN)
        {
            var carro = await dbContext.Carros.FirstOrDefaultAsync(x => x.VIN.Contains(VIN));

            if (carro == null)
            {
                return NotFound();
            }

            return carro;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Carro carro)
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
