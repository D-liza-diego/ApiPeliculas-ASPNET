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
    [Route("api/actores")]
    public class ActorController : ControllerBase
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;
        private readonly IAlmacenarArchivos almacenarArchivos;
        private readonly string contenedor = "actores";

        public ActorController(ApiContext apiContext, IMapper mapper, IAlmacenarArchivos almacenarArchivos)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
            this.almacenarArchivos = almacenarArchivos;
        }
     
        [HttpGet]
        public async Task<ActionResult<List<ActorDTO>>> Get()
        {
            var entidadActor = await apiContext.Actores.ToListAsync();
            return mapper.Map<List<ActorDTO>>(entidadActor);
        }
        [HttpGet("{id:int}", Name = "obtenerActor")]
        public async Task<ActionResult<ActorDTO>> Get(int id)
        {
            var entidadActor = await apiContext.Actores.FirstOrDefaultAsync(f => f.Id == id);
            if (entidadActor == null)
            {
                return NotFound();
            }
            return mapper.Map<ActorDTO>(entidadActor);
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromForm] ActorCreate actorCreate)
        {
            var entidadActor = mapper.Map<Actor>(actorCreate);
            //subir foto
            if(actorCreate.Foto!=null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await actorCreate.Foto.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(actorCreate.Foto.FileName);
                    entidadActor.Foto = await almacenarArchivos.GuardarArchivo(contenido, extension, 
                        contenedor, actorCreate.Foto.ContentType);
                }
            }
            apiContext.Add(entidadActor);
            await apiContext.SaveChangesAsync();
            var actorDTO = mapper.Map<ActorDTO>(entidadActor);
            return new CreatedAtRouteResult("obtenerActor", new { id = actorDTO.Id }, actorDTO);
        }
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromForm] ActorCreate actorCreate)
        {
            //var entidadActor = mapper.Map<Actor>(actorCreate);
            //entidadActor.Id = id;
            //apiContext.Entry(entidadActor).State = EntityState.Modified;

            var actorDB = await apiContext.Actores.FirstOrDefaultAsync(f => f.Id == id);
            if (actorDB == null)
            {
                return NotFound();
            }
            actorDB = mapper.Map(actorCreate, actorDB);

            if (actorCreate.Foto != null)
            {
                using (var memorystream = new MemoryStream())
                {
                    await actorCreate.Foto.CopyToAsync(memorystream);
                    var contenido = memorystream.ToArray();
                    var extension = Path.GetExtension(actorCreate.Foto.FileName);
                    actorDB.Foto = await almacenarArchivos.EditarArchivo(contenido, extension,
                        contenedor,actorDB.Foto, actorCreate.Foto.ContentType);
                }
            }
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpPatch("{id:int}")]
        public async Task<ActionResult> Patch (int id, [FromBody] JsonPatchDocument<ActorPatch> jsonPatchDocument)
        {
            if(jsonPatchDocument==null)
            {
                return BadRequest();
            }
            var actorBD = await apiContext.Actores.FirstOrDefaultAsync(f => f.Id == id);
            if(actorBD==null)
            {
                return NotFound();
            }
            var actorDTO = mapper.Map<ActorPatch>(actorBD);
            jsonPatchDocument.ApplyTo(actorDTO, ModelState);
            var validar = TryValidateModel(actorDTO);
            if(!validar)
            {
                return BadRequest(ModelState);
            }
            mapper.Map(actorDTO, actorBD);
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await apiContext.Actores.AnyAsync(a => a.Id == id);
            if(!exists)
            {
                return NotFound();
            }
            apiContext.Remove(new Actor() { Id = id }); 
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
    }
}
