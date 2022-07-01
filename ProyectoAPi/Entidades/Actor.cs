using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.Entidades
{
    public class Actor
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public string Foto { get; set; }
    }
}
