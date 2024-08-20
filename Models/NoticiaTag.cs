namespace DekaNews.Models;
public class NoticiaTag
{
    [Key]
    public int Id { get; set; }
    public Noticia Noticia { get; set; }
    public Tag Tag { get; set; }
}