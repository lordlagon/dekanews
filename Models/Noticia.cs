namespace DekaNews.Models;

[Table("Noticias")]
public class Noticia
{
    [Key]
    public int Id { get; set; }
    [Column(TypeName = "VARCHAR(250)")]
    public string Titulo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public string? UsuarioId { get; set; }
    public IdentityUser? Usuario { get; set; }
    public List<Tag> Tags { get; } = [];
}