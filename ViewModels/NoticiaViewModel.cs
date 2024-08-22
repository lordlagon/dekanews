namespace DekaNews.ViewModels;

public class NoticiaViewModel
{
    public int Id { get; set; } 
    public string Titulo { get; set; } = string.Empty;
    public string Texto { get; set; } = string.Empty;
    public List<SelectListItem> SelectedTags { get; set; } = [];
    public List<string?> TagIds { get; set; } = [];
}
