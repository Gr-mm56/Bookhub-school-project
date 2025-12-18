using WebMVC.Areas.Admin.Models.Book;
using WebMVC.Areas.Admin.Models.Image;

namespace WebMVC.Areas.Admin.Models.Author;

public class AuthorCreateEditViewModelWithOptions
{
    public required AuthorCreateEditViewModel Author { get; set; }

    public List<ImageOption> Images { get; set; } = [];
    public List<BookOption> Books { get; set; } = [];
}
