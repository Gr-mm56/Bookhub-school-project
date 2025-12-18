using WebMVC.Areas.Admin.Models.Image;
using WebMVC.Areas.Admin.Models.Book;

namespace WebMVC.Areas.Admin.Models.Publisher;

public class PublisherCreateEditViewModelWithOptions
{
    public required PublisherCreateEditViewModel Publisher { get; set; }

    public List<ImageOption> Images { get; set; } = [];
    public List<BookOption> Books { get; set; } = [];
}
