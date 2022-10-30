using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using WebAPI_Peliculas.Validaciones;

namespace WebAPI_Peliculas.Entidades
{
    public class Director
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(100, ErrorMessage = "El campo {0} debe contener máximo 100 caracteres")]
        [PrimeraLetraMayuscula(ErrorMessage = "Lol")]
        public string Nombre { get; set; }

        [Range(1, 150, ErrorMessage = "La edad debe ser un valor positivo y menor o igual a 150")]
        public int Edad { get; set; }
        public List<Pelicula> peliculas { get; set; }
    }
}
