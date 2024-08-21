using Microsoft.AspNetCore.Identity;

namespace DekaNews.Models;
[Table("Noticias")]
public class Noticia
{
    [Key]
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Texto { get; set; }
    public string? UsuarioId { get; set; }
    public IdentityUser? Usuario { get; set; }
}