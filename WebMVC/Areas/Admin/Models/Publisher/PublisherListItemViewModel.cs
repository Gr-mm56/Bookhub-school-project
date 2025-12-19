namespace WebMVC.Areas.Admin.Models.Publisher;

public class PublisherListItemViewModel
{
    public int Id { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public int BookCount { get; set; }
}