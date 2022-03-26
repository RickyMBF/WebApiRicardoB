using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRicardoB.Entities;

namespace WebApiRicardoB.Controllers
{
    [ApiController]
    [Route("api/paisesproductores")]

    public class PaisesProductoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PaisesProductoresController> log;
        public PaisesProductoresController(ApplicationDbContext context, ILogger<PaisesProductoresController> log)
        {
            this.dbContext = context;
            this.log = log;
        }

        [HttpGet]
        [HttpGet("/listadoPaisProductor")]
        public async Task<ActionResult<List<PaisProductor>>> GetAll()
        {
            log.LogInformation("Obteniendo listado de países productores.");
            return await dbContext.PaisesProductores.ToListAsync();
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<PaisProductor>> GetById(int id)
        {
            log.LogInformation("El ID es: " + id);
            return await dbContext.PaisesProductores.FirstOrDefaultAsync(x => x.Id == id);
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
            var exist = await dbContext.PaisesProductores.AnyAsync(x => x.Id == id);

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
            var exist = await dbContext.PaisesProductores.AnyAsync(x => x.Id == id);
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
