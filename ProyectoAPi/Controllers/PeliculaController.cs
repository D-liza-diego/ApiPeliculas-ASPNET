using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPi.DTO;
using ProyectoAPi.Entidades;
using ProyectoAPi.Services;
namespace ProyectoAPi.Controllers
{
    [ApiController]
    [Route("api/peliculas")]
    public class PeliculaController:ControllerBase
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;
        private readonly IAlmacenarArchivos almacenarArchivos;
        private readonly string contenedor = "Peliculas";

        public PeliculaController(ApiContext apiContext, IMapper mapper , IAlmacenarArchivos almacenarArchivos)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
            this.almacenarArchivos = almacenarArchivos;
        }
        [HttpGet]
        public async Task<ActionResult<List<PeliculaDTO>>> Get()
        {
            var peliculas = await apiContext.Peliculas.ToListAsync();
            return mapper.Map<List<PeliculaDTO>>(peliculas);
        }
        [HttpGet("{id:int}", Name ="obtenerPelicula")]
        public async Task<ActionResult<PeliculaDTO>> Get(int id)
        {
            var pelicula = await apiContext.Peliculas.FirstOrDefaultAsync(f=>f.Id==id);
            if(pelicula==null)
            {
                return NotFound();
            }
            return mapper.Map<PeliculaDTO>(pelicula);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm]PeliculaCreate peliculaCreate)
        {
            var pelicula = mapper.Map<Pelicula>(peliculaCreate);
            //subir foto
            if (peliculaCreate.poster != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await peliculaCreate.poster.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(peliculaCreate.poster.FileName);
                    pelicula.poster = await almacenarArchivos.GuardarArchivo(contenido, extension,
                        contenedor, peliculaCreate.poster.ContentType);
                }
            }
            apiContext.Add(pelicula);
            await apiContext.SaveChangesAsync();
            var peliculaDTO= mapper.Map<PeliculaDTO>(pelicula);
            return new CreatedAtRouteResult("obtenerPelicula", new { id = pelicula.Id }, peliculaDTO);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] PeliculaCreate peliculaCreate)
        {
            // var entidadActor = mapper.Map<Actor>(actorCreate);
            //entidadActor.Id = id;
            //apiContext.Entry(entidadActor).State = EntityState.Modified;

            var peliculaDB = await apiContext.Peliculas.FirstOrDefaultAsync(f => f.Id == id);
            if (peliculaDB == null)
            {
                return NotFound();
            }
            peliculaDB = mapper.Map(peliculaCreate, peliculaDB);

            if (peliculaCreate.poster != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await peliculaCreate.poster.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(peliculaCreate.poster.FileName);
                    peliculaDB.poster = await almacenarArchivos.EditarArchivo(contenido, extension,
                        contenedor, peliculaDB.poster, peliculaCreate.poster.ContentType);
                }
            }
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch(int id, [FromBody] JsonPatchDocument<PeliculaPatch> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest();
            }
            var peliculaDB = await apiContext.Peliculas.FirstOrDefaultAsync(f => f.Id == id);
            if (peliculaDB == null)
            {
                return NotFound();
            }
            var peliculaDTO = mapper.Map<PeliculaPatch>(peliculaDB);
            jsonPatchDocument.ApplyTo(peliculaDTO, ModelState);
            var validar = TryValidateModel(peliculaDTO);
            if (!validar)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(peliculaDTO, peliculaDB);
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await apiContext.Peliculas.AnyAsync(a => a.Id == id);
            if (!exists)
            {
                return NotFound();
            }
            apiContext.Remove(new Pelicula() { Id = id });
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
