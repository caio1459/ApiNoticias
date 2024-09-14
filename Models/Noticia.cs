namespace ApiNoticias.Models
{
    public class Noticia
    {
        public int NotId { get; set; }
        public required string Titulo { get; set; }
        public required string Texto { get; set; }
        public DateTime Data { get; set; }
        public int AutorId { get; set; }
        public  Autor? Autor { get; set; }
    }
}
