namespace WebMVC.Models
{
    public class HomePageViewModel
    {

        public List<BookCardViewModel> Books { get; set; } = [];
        
        public string? SearchQuery { get; set; }
        
        public int PageNumber { get; set; } = 1;
        
        public int PageSize { get; set; } = 12;
        
        public int TotalCount { get; set; }
        
        public int TotalPages { get; set; }
        
        public bool HasNextPage => PageNumber < TotalPages;
        
        public bool HasPreviousPage => PageNumber > 1;
    }
}

