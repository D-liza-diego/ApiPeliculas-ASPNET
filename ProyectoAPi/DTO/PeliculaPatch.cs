using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.DTO
{
    public class PeliculaPatch
    {
      
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        public bool disponible { get; set; }
        public DateTime FechaEstreno { get; set; }
    }
}
