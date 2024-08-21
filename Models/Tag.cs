namespace DekaNews.Models;
[Table("Tags")]

public class Tag
{
    [Key]
    public int Id { get; set; }
    public string Descricao { get; set; } = string.Empty;
    public List<Noticia> Noticias { get; } = [];
}