using System.ComponentModel.DataAnnotations;

namespace WebMVC.Models
{
    public class BookSearchViewModel
    {
        [MaxLength(200)]
        [Display(Name = "Search Books")]
        public string? SearchTerm { get; set; }

        public int PageNumber { get; set; } = 1;

        public int PageSize { get; set; } = 12;
    }
}

