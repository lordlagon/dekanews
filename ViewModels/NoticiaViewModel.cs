using Microsoft.AspNetCore.Mvc.Rendering;

namespace DekaNews.ViewModels;

public class NoticiaViewModel
{
    public string Titulo { get; set; }
    public string Texto { get; set; }    
    public MultiSelectList Tags { get; set; }
}
