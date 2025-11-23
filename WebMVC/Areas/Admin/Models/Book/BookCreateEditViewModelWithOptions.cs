namespace WebMVC.Areas.Admin.Models.Book;

public class BookCreateEditViewModelWithOptions
{
    public required BookCreateEditViewModel Book { get; set; }

    public List<GenreOption> Genres { get; set; } = [];
    public List<ImageOption> Images { get; set; } = [];
    public List<PublisherOption> Publishers { get; set; } = [];
    public List<AuthorOption> Authors { get; set; } = [];
}

public class GenreOption
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class ImageOption
{
    public int Id { get; set; }
    public required string FileName { get; set; }
}

public class PublisherOption
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

public class AuthorOption
{
    public int Id { get; set; }
    public required string Name { get; set; }
}

