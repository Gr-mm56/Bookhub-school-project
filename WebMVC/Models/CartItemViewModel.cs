namespace WebMVC.Models;

public class CartItemViewModel
{
    public int BookId { get; set; }

    public string Title { get; set; } = "";

    public double Price { get; set; }

    public int Quantity { get; set; }

    public double Subtotal => Price * Quantity;
}
