using BusinessLayer.Models.Common;

namespace WebMVC.Models
{
    public class HomePageViewModel
    {
        public List<BookCardViewModel> Books { get; set; } = [];

        public List<AuthorCardViewModel> Authors { get; set; } = [];

        public List<PublisherCardViewModel> Publishers { get; set; } = [];

        public string? SearchQuery { get; set; }

        public PaginationInfo BookPagination { get; set; } = new(pageSize: 12);

        public PaginationInfo AuthorPagination { get; set; } = new(pageSize: 5);

        public PaginationInfo PublisherPagination { get; set; } = new(pageSize: 5);

        public bool IsSignedIn { get; set; }
    }
}
