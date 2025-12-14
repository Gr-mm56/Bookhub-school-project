namespace WebMVC.Models;

public class WishlistItemViewModel
{
    public int Id { get; set; }

    public int BookId { get; set; }

    public string Title { get; set; } = "";

    public double Price { get; set; }
}
