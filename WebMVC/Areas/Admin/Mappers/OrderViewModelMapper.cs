using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Models.User.Responses;
using DataAccessLayer.Entities;
using WebMVC.Areas.Admin.Models.Order;

namespace WebMVC.Areas.Admin.Mappers;

public static class OrderViewModelMapper
{
    public static OrderViewModel ToListViewModel(ICollection<CartDto> orderDtos)
    {
        ArgumentNullException.ThrowIfNull(orderDtos);

        return new OrderViewModel
        {
            Orders = orderDtos
                .OrderBy(o => o.OrderId)
                .Select(ToListItemViewModel)
                .ToList()
        };
    }

    public static OrderListItemViewModel ToListItemViewModel(CartDto orderDto)
    {
        ArgumentNullException.ThrowIfNull(orderDto);

        return new OrderListItemViewModel
        {
            Id = orderDto.Id,
            TotalValue = orderDto.TotalValue,
            CreatedAt = orderDto.CreatedAt,
            UpdatedAt = orderDto.UpdatedAt,
        };
    }

    public static CartCreateDto ToCreateDto(OrderCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new CartCreateDto
        {
            UserId = viewModel.UserId,
            TotalValue = viewModel.TotalValue,
            OrderId = viewModel.OrderId,
            OrderDate = viewModel.OrderDate,
        };
    }

    public static OrderCreateEditViewModelWithOptions ToCreateEditViewModelWithOptions(
        OrderCreateEditViewModel orderViewModel,
        UserDetailDto user,
        ICollection<PurchaseItemDetailDto> purchaseItems,
        ICollection<BookDto> books
    ) {
        ArgumentNullException.ThrowIfNull(orderViewModel);
        ArgumentNullException.ThrowIfNull(user);
        ArgumentNullException.ThrowIfNull(purchaseItems);
        ArgumentNullException.ThrowIfNull(books);

        return new OrderCreateEditViewModelWithOptions
        {
            Order = orderViewModel,
            User = user, // TODO USER to USEROPTION
            PurchaseItems = purchaseItems
                .Select(pi => new PurchaseItemOption
                {
                    Id = pi.Id,
                    BookId = pi.Book.Id,
                    BookTitle = pi.Book.Title,
                    BookPrice = pi.Book.Price,
                    Count = pi.Count
                })
                .OrderBy(x => x.BookTitle)
                .ToList()
        };
    }

    public static CartUpdateDto ToUpdateDto(OrderCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new CartUpdateDto
        {
            TotalValue = viewModel.TotalValue,
            OrderId = viewModel.OrderId,
            OrderDate = viewModel.OrderDate,
        };
    }

    public static OrderDeleteViewModel ToDeleteViewModel(CartDetailDto orderDetailDto)
    {
        ArgumentNullException.ThrowIfNull(orderDetailDto);

        return new OrderDeleteViewModel
        {
            Id = orderDetailDto.Id,
            TotalValue = orderDetailDto.TotalValue,
            OrderId = orderDetailDto.OrderId,
            OrderDate = orderDetailDto.OrderDate,
            CreatedAt = orderDetailDto.CreatedAt,
            UpdatedAt = orderDetailDto.UpdatedAt,
            PurchaseItemsCount = orderDetailDto.PurchaseItems?.Count ?? 0
        };
    }
}
