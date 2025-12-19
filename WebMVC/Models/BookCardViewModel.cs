namespace WebMVC.Models;

public class BookCardViewModel

{
    public string? FirstAuthorName { get; set; }

    public string? ImageUrl { get; set; }

    public double Price { get; set; }

    public required string Title { get; set; }

    public int Id { get; set; }
}
