namespace WebMVC.Areas.Admin.Models.Image;

public class ImageListItemViewModel
{
    public int Id { get; set; }

    public string FileUrl { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
}
