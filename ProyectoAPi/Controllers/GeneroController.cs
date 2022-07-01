using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProyectoAPi.DTO;
using ProyectoAPi.Entidades;
namespace ProyectoAPi.Controllers
{
    [ApiController]
    [Route("api/generos")]
    public class GeneroController : ControllerBase
    {
        private readonly ApiContext apiContext;
        private readonly IMapper mapper;

        public GeneroController(ApiContext apiContext, IMapper mapper)
        {
            this.apiContext = apiContext;
            this.mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var EntidadGenero = await apiContext.Generos.ToListAsync();
            var dto = mapper.Map<List<GeneroDTO>>(EntidadGenero);
            return dto;
        }
        [HttpGet("{id:int}", Name = "obtener")]
        public async Task<ActionResult<GeneroDTO>> Get(int id)
        {
            var EntidadGenero = await apiContext.Generos.FirstOrDefaultAsync(g => g.id == id);
            if (EntidadGenero == null)
            {
                return NotFound();
            }
            var dto = mapper.Map<GeneroDTO>(EntidadGenero);
            return dto;
        }
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreate generoCreate)
        {
            var EntidadGenero = mapper.Map<Genero>(generoCreate);
            apiContext.Add(EntidadGenero);
            await apiContext.SaveChangesAsync();

            var generoDTO = mapper.Map<GeneroDTO>(EntidadGenero);
            return new CreatedAtRouteResult("obtener", new { id = generoDTO.id }, generoDTO);
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] GeneroCreate generoCreate)
        {
            var EntidadGenero = mapper.Map<Genero>(generoCreate);
            EntidadGenero.id= id;
            apiContext.Entry(EntidadGenero).State = EntityState.Modified;
            await apiContext.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var exists = await apiContext.Generos.AnyAsync(a=>a.id==id);
            if(!exists)
            {
                return NotFound();
            }
            apiContext.Remove(new Genero() { id = id });
            await apiContext.SaveChangesAsync();
            return Ok();
        }
    }
}
