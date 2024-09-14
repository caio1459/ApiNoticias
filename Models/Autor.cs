namespace ApiNoticias.Models
{
    public class Autor
    {
        public int AutorID { get; set; }
        public required string Nome { get; set; }
        public required string Email { get; set; }
    }
}
