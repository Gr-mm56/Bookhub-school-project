namespace WebMVC.Areas.Admin.Models.Genre;

public class GenreListItemViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
