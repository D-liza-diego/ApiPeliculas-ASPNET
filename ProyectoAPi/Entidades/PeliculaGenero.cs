namespace ProyectoAPi.Entidades
{
    public class PeliculaGenero
    {
        public int generoid { get; set; }
        public int peliculaid { get; set; }
        public Genero genero { get; set; }
        public Pelicula pelicula { get; set; }
    }
}
