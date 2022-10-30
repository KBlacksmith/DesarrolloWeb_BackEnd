using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas.Controllers
{
    [ApiController]
    [Route("api/directores")]
    public class DirectoresController : ControllerBase
    {
        private readonly ApplicationDbContext dbContext;
        public DirectoresController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Director>>> GetAll()
        {
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
