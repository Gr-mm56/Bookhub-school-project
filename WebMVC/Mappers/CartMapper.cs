using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.PurchaseItem.Responses;
using WebMVC.Models;

namespace WebMVC.Mappers;

public static class CartMapper
{
    public static List<CartViewModel> ToCartViewModels(IEnumerable<CartDetailDto> carts)
    {
        return carts.Select(ToCartViewModel).ToList();
    }

    public static CartViewModel ToCartViewModel(CartDetailDto cart)
    {
        return new CartViewModel
        {
            Id = cart.Id,
            User = cart.User,
            PaymentStatus = cart.PaymentStatus,
            PurchaseItems = cart.PurchaseItems?
                .OfType<PurchaseItemDetailDto>()
                .Select(pi => new CartItemViewModel
                {
                    BookId = pi.BookId,
                    Title = pi.Book.Title,
                    Price = pi.Book.Price,
                    Quantity = pi.Count
                })
                .ToList()
            ?? new List<CartItemViewModel>()
        };
    }
}
