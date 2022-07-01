using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.DTO
{
    public class GeneroDTO
    {
        public int id { get; set; }
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
