using BusinessLayer.Models.WishlistItem.Responses;
using WebMVC.Models;

namespace WebMVC.Mappers;

public static class WishlistMapper
{
    public static List<WishlistItemViewModel> ToWishlistViewModels(IEnumerable<WishlistItemDetailDto> wishlists)
    {
        return wishlists.Select(ToWishlistViewModel).ToList();
    }

    public static WishlistItemViewModel ToWishlistViewModel(WishlistItemDetailDto wishlist)
    {
        return new WishlistItemViewModel
        {
            Id = wishlist.Id,
            BookId = wishlist.BookId,
            Title = wishlist.Book.Title,
            Price = wishlist.Book.Price,
        };
    }
}
