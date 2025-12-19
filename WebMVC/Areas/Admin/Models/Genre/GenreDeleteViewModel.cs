using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Models.Genre;

public class GenreDeleteViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public List<BookListItemBasicViewModel> PrimaryBooks { get; set; } = [];

    public List<BookListItemBasicViewModel> AssociatedBooks { get; set; } = [];
}
