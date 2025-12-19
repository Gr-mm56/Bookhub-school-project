namespace WebMVC.Areas.Admin.Models.Author;

public class AuthorDeleteViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}