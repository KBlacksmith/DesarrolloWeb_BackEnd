using Microsoft.EntityFrameworkCore;
using WebAPI_Peliculas.Entidades;

namespace WebAPI_Peliculas
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions options): base(options)
        {

        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Director> Directores { get; set; }
    }
}
