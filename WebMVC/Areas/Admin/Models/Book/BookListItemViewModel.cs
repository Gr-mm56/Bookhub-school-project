namespace WebMVC.Areas.Admin.Models.Book;

public class BookListItemViewModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required double Price { get; set; }
}