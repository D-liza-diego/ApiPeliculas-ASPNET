using AutoMapper;
using ProyectoAPi.Entidades;
using ProyectoAPi.DTO;
namespace ProyectoAPi.Helpers
{
    public class AutoMapperProfile:Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Genero, GeneroDTO>().ReverseMap();
            CreateMap<GeneroCreate, Genero>();

            CreateMap<Actor, ActorDTO>().ReverseMap();
            CreateMap<ActorCreate, Actor>().ForMember(x=>x.Foto, options=>options.Ignore());
            CreateMap<ActorPatch, Actor>().ReverseMap();

            CreateMap<Pelicula, PeliculaDTO>().ReverseMap();
            CreateMap<PeliculaCreate, Pelicula>().ForMember(x => x.poster, options => options.Ignore());
            CreateMap<PeliculaPatch, Pelicula>().ReverseMap();
        }
    }
}
