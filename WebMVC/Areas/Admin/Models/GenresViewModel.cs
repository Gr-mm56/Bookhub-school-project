using BusinessLayer.Models.Genre.Responses;

namespace WebMVC.Areas.Admin.Models;

public class GenresViewModel
{
    public List<GenreDto>? Genres { get; set; }
}