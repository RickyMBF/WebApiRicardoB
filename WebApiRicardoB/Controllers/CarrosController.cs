using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiRicardoB.Entities;
using WebApiRicardoB.Filtros;
using WebApiRicardoB.Services;

namespace WebApiRicardoB.Controllers
{
    [ApiController]
    [Route("api/carros")]
    //[Authorize]
    public class CarrosController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<CarrosController> logger;

        public CarrosController(ApplicationDbContext context, ServiceA service,
           ServiceTransient serviceTransient, ServiceScoped serviceScoped,
           ServiceSingleton serviceSingleton, ILogger<CarrosController> logger)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        //[ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            return Ok(new
            {
                CarrosControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                CarrosControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                CarrosController_Singleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet] //api/carro
        [HttpGet("listado")] //api/carros/listado
        [HttpGet("/listado")] // /listado
        [ResponseCache(Duration = 15)]
        //[Authorize]
        //[ServiceFilter(typeof(FiltroDeAccion))]
        public async Task<ActionResult<List<Carro>>> Get()
        {
            //* Niveles de logs
            // Critical
            // Error
            // Warning
            // Information
            // Debug
            // Trace
            // *//
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de carros.");
            logger.LogWarning("Mensaje de prueba warning.");
            service.EjecutarJob();
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

        [HttpGet("obtenerCarro/{VIN}")]
        public async Task<ActionResult<Carro>> Get([FromRoute] string VIN)
        {
            var carro = await dbContext.Carros.FirstOrDefaultAsync(x => x.VIN.Contains(VIN));

            if (carro == null)
            {
                logger.LogError("No se encuentra al carro.");
                return NotFound();
            }

            return carro;
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Carro carro)
        {
            //Ejemplo para validar desde el controlador con la BD con ayuda del dbContext

            var existeCarroMismoVIN = await dbContext.Carros.AnyAsync(x => x.VIN == carro.VIN);

            if(existeCarroMismoVIN)
            {
                return BadRequest("Ya existe un carro con el mismo VIN.");
            }

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
