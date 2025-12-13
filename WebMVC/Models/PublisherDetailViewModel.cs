namespace WebMVC.Models;

public class PublisherDetailViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public string? ImageUrl { get; set; }
    public List<BookCardViewModel> Books { get; set; } = [];
}