using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;
using WebAPI_Peliculas.Services;
using WebAPI_Peliculas.Filtros;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/directores")]
    public class DirectoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IService service;
        private readonly ServiceTransient serviceTransient;
        private readonly ServiceScoped serviceScoped;
        private readonly ServiceSingleton serviceSingleton;
        private readonly ILogger<DirectoresController> logger;
        private readonly IWebHostEnvironment env;
        private readonly string nuevosRegistros = "nuevosRegistros.txt";
        private readonly string registrosConsultados = "registrosConsultados.txt";
        public DirectoresController(ApplicationDbContext context, IService service, 
        ServiceTransient serviceTransient, ServiceScoped serviceScoped, 
        ServiceSingleton serviceSingleton, ILogger<DirectoresController> logger, 
        IWebHostEnvironment env)
        {
            this.dbContext = context;
            this.service = service;
            this.serviceTransient = serviceTransient;
            this.serviceScoped = serviceScoped;
            this.serviceSingleton = serviceSingleton;
            this.logger = logger;
            this.env = env;
        }

        [HttpGet("GUID")]
        [ResponseCache(Duration = 10)]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public ActionResult ObtenerGuid()
        {
            throw new NotImplementedException();
            logger.LogInformation("Durante la ejecución");
            return Ok(new{
                DirectoresControllerTransient = serviceTransient.guid,
                ServiceA_Transient = service.GetTransient(),
                DirectoresControllerScoped = serviceScoped.guid,
                ServiceA_Scoped = service.GetScoped(),
                DirectoresControllerSingleton = serviceSingleton.guid,
                ServiceA_Singleton = service.GetSingleton()
            });
        }

        [HttpGet]
        [ServiceFilter(typeof(FiltroDeAccion))]
        public async Task<ActionResult<List<Director>>> GetAll()
        {
            throw new NotImplementedException();
            logger.LogInformation("Se obtiene el listado de películas");
            logger.LogWarning("Mensaje de prueba warning");
            service.EjecutarJob();
            return await dbContext.Directores.Include(x => x.peliculas).ToListAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Director>> GetById(int id)
        {
            return await dbContext.Directores.FirstOrDefaultAsync(x => x.Id == id);
        }
        [HttpPost]
        public async Task<ActionResult> Post(Director director)
        {
            var existeDirector = await dbContext.Directores.AnyAsync(x => x.Nombre == director.Nombre);
            if (existeDirector)
            {
                return BadRequest("Ya existe un director con ese nombre");
            }
            dbContext.Add(director);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(Director director, int id)
        {
            var exists = await dbContext.Directores.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("El director especificado no existe");
            }
            if(director.Id != id)
            {
                return BadRequest("El id del director no coincide con el establecido en la url");
            }
            dbContext.Update(director);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await dbContext.Directores.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("No se encontró el director con id "+id.ToString());
            }
            dbContext.Remove(new Director { Id = id });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
