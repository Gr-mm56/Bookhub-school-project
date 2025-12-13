using BusinessLayer.Models.User.Responses;

namespace WebMVC.Models
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public double TotalValue =>
            PurchaseItems?.Sum(i => i.Subtotal) ?? 0;

        public UserDto User { get; set; }

        public ICollection<CartItemViewModel>? PurchaseItems { get; set; }

        public int PaymentStatus { get; set; }
    }
}
