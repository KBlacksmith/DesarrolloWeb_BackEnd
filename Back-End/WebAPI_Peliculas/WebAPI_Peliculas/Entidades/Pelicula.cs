using System.ComponentModel.DataAnnotations;

namespace WebAPI_Peliculas.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        public string Titulo { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Ingrese un valor mayor o igual a 1 y menor a {1}")]
        public int Duracion { get; set; }//Minutos
        public int DirectorId { get; set; }
        public Director Director { get; set; }
    }
}
