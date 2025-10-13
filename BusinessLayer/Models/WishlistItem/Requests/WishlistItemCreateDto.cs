namespace BusinessLayer.Models.WishlistItem.Requests;

public class WishlistItemCreateDto
{
    public int UserId { get; set; }

    public int BookId { get; set; }
}
