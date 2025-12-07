namespace WebMVC.Models;

public class AuthorDetailViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? ImageUrl { get; set; }
    public List<BookCardViewModel> Books { get; set; } = [];
}