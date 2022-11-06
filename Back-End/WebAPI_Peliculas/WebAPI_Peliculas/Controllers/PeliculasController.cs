using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculasController: ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        private readonly ILogger<PeliculasController> log;
        public PeliculasController(ApplicationDbContext context, ILogger<PeliculasController> log)
        {
            this.dbContext = context;
            this.log = log;
        }

        [HttpGet]
        [HttpGet("listado")]
        [HttpGet("/listado")]
        public async Task<ActionResult<List<Pelicula>>> Get()
        {
            log.LogInformation("Obteniendo listado de películas");

            /* return await dbContext.Peliculas.Include(x => x.Titulo).ToListAsync();*/
            return await dbContext.Peliculas.ToListAsync();
        }
        [HttpGet("primero")]
        public async Task<ActionResult<Pelicula>> GetPrimerPelicula()
        {
            //var vacio = await dbContext.Peliculas.
            return await dbContext.Peliculas.FirstOrDefaultAsync();
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Pelicula>> GetById(int id)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if(pelicula == null)
            {
                return NotFound("No se encontró la película con id " + id.ToString());
            }
            log.LogInformation("El ID es: "+id.ToString());
            return pelicula;
        }
        [HttpGet("{titulo}")]
        public async Task<ActionResult<Pelicula>> GetByTitle([FromRoute] string titulo)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Titulo == titulo);
            if(pelicula == null)
            {
                return NotFound("No se encontró la película \"" + titulo + "\"");
            }
            return pelicula;
        }
        [HttpGet("{param?}")]
        public async Task<ActionResult<Pelicula>> GetParam(int id, string param)
        {
            var pelicula = await dbContext.Peliculas.FirstOrDefaultAsync(x => x.Id == id);
            if(pelicula == null)
            {
                return NotFound("No se encontró la película con id '" + id.ToString() + "'");
            }
            return pelicula;
        }
        
        [HttpPost]
        public async Task<ActionResult> Post(Pelicula pelicula)
        {
            var existeDirector = await dbContext.Directores.AnyAsync(x => x.Id == pelicula.DirectorId);
            if (!existeDirector)
            {
                return BadRequest("No existe el director con el ID "+pelicula.DirectorId.ToString());
            }
            dbContext.Add(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }
        [HttpPut("{id:int}")]//api/peliculas/1
        public async Task<ActionResult> Put(Pelicula pelicula, int id)
        {
            var exists = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("La pelicula especificado no existe");
            }
            if (pelicula.Id != id)
            {
                return BadRequest("El id de la película no coincide con el establecido en la url");
            }
            dbContext.Update(pelicula);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await dbContext.Peliculas.AnyAsync(x => x.Id == id);
            if (!exists)
            {
                return NotFound("No se encontró una película con id "+id.ToString());
            }
            dbContext.Remove(new Pelicula()
            {
                Id = id,
            });
            await dbContext.SaveChangesAsync();
            return Ok();
        }
    }
}
