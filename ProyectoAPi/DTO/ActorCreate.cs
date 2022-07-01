using ProyectoAPi.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.DTO
{
    public class ActorCreate
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string Name { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [ImageSize(4)]
        [TypeFile(tipoGrupos:TipoGrupos.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
