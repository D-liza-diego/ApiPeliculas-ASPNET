using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.Entidades
{
    public class Pelicula
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        public bool disponible { get; set; }
        public DateTime FechaEstreno { get; set; }
        public string poster { get; set; }
    }
}
