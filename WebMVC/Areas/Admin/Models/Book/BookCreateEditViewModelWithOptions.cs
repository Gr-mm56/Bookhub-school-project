using WebMVC.Areas.Admin.Models.Author;
using WebMVC.Areas.Admin.Models.Genre;
using WebMVC.Areas.Admin.Models.Image;
using WebMVC.Areas.Admin.Models.Publisher;

namespace WebMVC.Areas.Admin.Models.Book;

public class BookCreateEditViewModelWithOptions
{
    public required BookCreateEditViewModel Book { get; set; }

    public List<GenreOption> Genres { get; set; } = [];

    public List<ImageOption> Images { get; set; } = [];

    public List<PublisherOption> Publishers { get; set; } = [];

    public List<AuthorOption> Authors { get; set; } = [];
}
