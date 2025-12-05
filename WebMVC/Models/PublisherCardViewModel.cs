namespace WebMVC.Models;

public class PublisherCardViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public string? ProfilePhotoUrl { get; set; }
    public int BookCount { get; set; }
}