namespace BusinessLayer.Models.WishlistItem.Requests;

public class WishlistItemDto
{
    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
