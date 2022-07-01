using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.DTO
{
    public class GeneroCreate
    {
        [Required]
        [StringLength(40)]
        public string Nombre { get; set; }
    }
}
