using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.PurchaseItem.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class CartMapper
{
    public static CartDto ToDto(Cart cart)
    {
        ArgumentNullException.ThrowIfNull(cart);

        return new CartDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            TotalValue = cart.TotalValue,
            OrderId = cart.OrderId,
            OrderDate = cart.OrderDate,
            PaymentStatus = cart.PaymentStatus,
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt,
        };
    }

    public static CartDetailDto ToDetailDto(Cart cart)
    {
        ArgumentNullException.ThrowIfNull(cart);

        return new CartDetailDto
        {
            Id = cart.Id,
            UserId = cart.UserId,
            User = UserMapper.ToDto(cart.User),
            TotalValue = cart.TotalValue,
            OrderId = cart.OrderId,
            OrderDate = cart.OrderDate,
            PurchaseItems = cart.PurchaseItems?.Select(PurchaseItemMapper.ToDetailDto).ToList() ?? new List<PurchaseItemDetailDto>(),
            PaymentStatus = cart.PaymentStatus,
            CreatedAt = cart.CreatedAt,
            UpdatedAt = cart.UpdatedAt,
        };
    }

    public static Cart CreateDtoToEntity(CartCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new Cart
        {
            UserId = createDto.UserId,
            TotalValue = createDto.TotalValue,
            OrderId = createDto.OrderId,
            OrderDate = createDto.OrderDate,
            PaymentStatus = createDto.PaymentStatus,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }

    public static Cart DetailDtoToEntity(CartDetailDto cartDetailDto)
    {
        ArgumentNullException.ThrowIfNull(cartDetailDto);

        return new Cart
        {
            UserId = cartDetailDto.UserId,
            TotalValue = cartDetailDto.TotalValue,
            OrderId = cartDetailDto.OrderId,
            OrderDate = cartDetailDto.OrderDate,
            PaymentStatus = cartDetailDto.PaymentStatus,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }

    public static void UpdateEntity(Cart cart, CartUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(cart);
        ArgumentNullException.ThrowIfNull(updateDto);

        cart.TotalValue = updateDto.TotalValue;
        cart.OrderId = updateDto.OrderId;
        cart.OrderDate = updateDto.OrderDate;
        cart.PaymentStatus = updateDto.PaymentStatus;
        cart.UpdatedAt = DateTime.Now;
    }

    public static Cart CreateOrderDtoToEntity(OrderCreateDto createDto)
    {
        ArgumentNullException.ThrowIfNull(createDto);

        return new Cart
        {
            UserId = createDto.UserId,
            TotalValue = createDto.TotalValue,
            OrderId = createDto.OrderId,
            OrderDate = createDto.OrderDate,
            PaymentStatus = createDto.PaymentStatus,
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
        };
    }

    public static void UpdateOrderEntity(Cart order, OrderUpdateDto updateDto)
    {
        ArgumentNullException.ThrowIfNull(order);
        ArgumentNullException.ThrowIfNull(updateDto);

        order.TotalValue = updateDto.TotalValue;
        order.UserId = updateDto.UserId;
        order.OrderDate = updateDto.OrderDate;
        order.PaymentStatus = updateDto.PaymentStatus;
        order.UpdatedAt = DateTime.Now;
    }

    public static IEnumerable<CartDto> ToDtoList(IEnumerable<Cart> carts)
    {
        return carts.Select(ToDto);
    }

    public static IEnumerable<CartDetailDto> ToDetailDtoList(IEnumerable<Cart> carts)
    {
        return carts.Select(ToDetailDto);
    }
}
