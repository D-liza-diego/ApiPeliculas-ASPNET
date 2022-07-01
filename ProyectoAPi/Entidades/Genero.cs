using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.Entidades
{
    public class Genero
    {
        public int id { get; set; }
        [Required]
        [StringLength(40)]

        public string Nombre { get; set; }
    }
}
