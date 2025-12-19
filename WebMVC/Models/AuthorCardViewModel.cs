namespace WebMVC.Models;

public class AuthorCardViewModel
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public required string Surname { get; set; }

    public string? ProfilePhotoUrl { get; set; }

    public int BookCount { get; set; }
}
