namespace WebMVC.Areas.Admin.Models.Author;

public class AuthorCreateEditViewModelWithOptions
{
    public required AuthorCreateEditViewModel Author { get; set; }

    public List<ImageOption> Images { get; set; } = [];
    public List<BookOption> Books { get; set; } = [];
}

public class ImageOption
{
    public int Id { get; set; }
    public required string FileName { get; set; }
}

public class BookOption
{
    public int Id { get; set; }
    public required string Title { get; set; }
}