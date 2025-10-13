namespace BusinessLayer.Models.WishlistItem.Responses;

public class WishlistItemDto
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BookId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }
}
