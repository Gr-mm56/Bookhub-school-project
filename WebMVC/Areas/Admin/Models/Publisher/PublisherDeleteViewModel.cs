namespace WebMVC.Areas.Admin.Models.Publisher;

public class PublisherDeleteViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}