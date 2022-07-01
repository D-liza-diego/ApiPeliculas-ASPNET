using Microsoft.AspNetCore.Mvc;
using ProyectoAPi.Helpers;
using ProyectoAPi.Validation;
using System.ComponentModel.DataAnnotations;

namespace ProyectoAPi.DTO
{
    public class PeliculaCreate
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string titulo { get; set; }
        public bool disponible { get; set; }
        public DateTime FechaEstreno { get; set; }

        [ImageSize(4)]
        [TypeFile(tipoGrupos: TipoGrupos.Imagen)]
        public IFormFile poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GeneroIDs { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorCreate>>))]
        public List<ActorCreate> Actores { get; set; }
    }
}
