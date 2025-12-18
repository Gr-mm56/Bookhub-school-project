namespace BusinessLayer.Models.GiftCard.Responses;

public class GiftCardDto
{
    public int Id { get; set; }
    public double PriceReduction { get; set; }
    public DateTime ValidFrom { get; set; }
    public DateTime ValidTo { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}