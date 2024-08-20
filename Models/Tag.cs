namespace DekaNews.Models;

public class Tag
{
    [Key]
    public int Id { get; set; }
    public string Descricao { get; set; }
}