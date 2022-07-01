namespace ProyectoAPi.Entidades
{
    public class PeliculaActor
    {
        public int actorid { get; set; }
        public int peliculaid { get; set; }
        public string personaje { get; set; }
        public int orden { get; set; }
        public Actor actor { get; set; }
        public Pelicula pelicula { get; set; }
    }
}
