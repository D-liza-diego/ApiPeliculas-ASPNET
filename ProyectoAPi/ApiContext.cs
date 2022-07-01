using Microsoft.EntityFrameworkCore;
using ProyectoAPi.Entidades;
namespace ProyectoAPi
{
    public class ApiContext : DbContext
    {
        public ApiContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PeliculaActor>().HasKey(
                x => new { x.actorid, x.peliculaid });
            modelBuilder.Entity<PeliculaGenero>().HasKey(
               x => new { x.peliculaid, x.generoid });
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<Actor> Actores { get; set; }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<PeliculaActor> PeliculaActores {get;set;}
        public DbSet<PeliculaGenero> PeliculaGeneros { get; set; }
    }
}
