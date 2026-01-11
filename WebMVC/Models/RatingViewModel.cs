namespace WebMVC.Models;

public class RatingViewModel
{
    public int Id { get; set; }

    public int Stars { get; set; }
    
    public int UserId { get; set; }

    public DateTime CreatedAt { get; set; }
}