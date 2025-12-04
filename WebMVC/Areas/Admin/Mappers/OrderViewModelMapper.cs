using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Models.User.Responses;
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
            OrderId = orderDto.OrderId,
            OrderDate = orderDto.OrderDate,
            PaymentStatus = orderDto.PaymentStatus,
            CreatedAt = orderDto.CreatedAt,
            UpdatedAt = orderDto.UpdatedAt,
        };
    }

    public static OrderCreateDto ToCreateDto(OrderCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new OrderCreateDto
        {
            UserId = viewModel.UserId,
            TotalValue = viewModel.TotalValue,
            OrderDate = viewModel.OrderDate,
            PaymentStatus = viewModel.PaymentStatus,
            BookIds = viewModel.BookIds
        };
    }

    public static OrderCreateEditViewModelWithOptions ToCreateEditViewModelWithOptions(
        OrderCreateEditViewModel orderViewModel,
        ICollection<UserDto> users,
        ICollection<PurchaseItemDetailDto> purchaseItems,
        ICollection<BookDto> books
    ) {
        ArgumentNullException.ThrowIfNull(orderViewModel);
        ArgumentNullException.ThrowIfNull(users);
        ArgumentNullException.ThrowIfNull(purchaseItems);
        ArgumentNullException.ThrowIfNull(books);

        return new OrderCreateEditViewModelWithOptions
        {
            Order = orderViewModel,
            Users = users
                .Select(u => new UserOption
                {
                    Id = u.Id,
                    Name = u.Name + " " + u.Surname,
                })
                .OrderBy(u => u.Id)
                .ToList(),
            PurchaseItems = purchaseItems
                .Select(pi => new PurchaseItemOption
                {
                    Id = pi.Id,
                    BookId = pi.Book.Id,
                    BookTitle = pi.Book.Title,
                    BookPrice = pi.Book.Price,
                    Count = pi.Count
                })
                .OrderBy(pi => pi.BookTitle)
                .ToList()
        };
    }

    public static OrderUpdateDto ToUpdateDto(OrderCreateEditViewModel viewModel)
    {
        ArgumentNullException.ThrowIfNull(viewModel);

        return new OrderUpdateDto
        {
            TotalValue = viewModel.TotalValue,
            OrderDate = viewModel.OrderDate,
            PaymentStatus = viewModel.PaymentStatus,
            UserId = viewModel.UserId,
            BookIds = viewModel.BookIds,
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
            PaymentStatus = orderDetailDto.PaymentStatus,
            CreatedAt = orderDetailDto.CreatedAt,
            UpdatedAt = orderDetailDto.UpdatedAt,
            PurchaseItemsCount = orderDetailDto.PurchaseItems?.Count ?? 0
        };
    }
}
