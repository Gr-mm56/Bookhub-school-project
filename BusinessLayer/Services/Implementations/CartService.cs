using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class CartService : BaseService<BookHubDbContext>, ICartService
{
    public CartService(BookHubDbContext context): base(context)
    {
    }

    public async Task<PagedResultDto<CartDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Carts
            .AsNoTracking()
            .OrderBy(c => c.Id);

        return await PageAsync(query, limit, offset, CartMapper.ToDtoList);
    }

    /**
     * Get Carts that had orderId filled, those are actual orders
     */
    public async Task<PagedResultDto<CartDto>> GetAllOrdersAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Carts
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.PurchaseItems)
            .Where(c => c.OrderId != null)
            .OrderBy(c => c.OrderDate);

        return await PageAsync(query, limit, offset, CartMapper.ToDtoList);
    }

    public async Task<CartDetailDto?> GetByIdAsync(int id)
    {
        var cart = await Context.Carts
            .AsNoTracking()
            .Include(c => c.User)
            .Include(c => c.PurchaseItems)
            .FirstOrDefaultAsync(c => c.Id == id);

        return cart != null ? CartMapper.ToDetailDto(cart) : null;
    }

    public async Task<CartDto> CreateAsync(CartCreateDto cartCreateDto)
    {
        // Validate that User exists
        await ValidateRelatedEntitiesExistAsync(cartCreateDto.UserId);

        // Validate other values
        await ValidateValues(cartCreateDto.OrderId, cartCreateDto.TotalValue);

        Cart cart = CartMapper.CreateDtoToEntity(cartCreateDto);

        await Context.Carts.AddAsync(cart);
        await SaveAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task<CartDto> CreateOrderAsync(OrderCreateDto orderCreateDto)
    {
        // Validate that User exists
        await ValidateRelatedEntitiesExistAsync(orderCreateDto.UserId);

        // Validate other values
        await ValidateValues(orderCreateDto.OrderId, orderCreateDto.TotalValue);

        Cart cart = CartMapper.CreateOrderDtoToEntity(orderCreateDto);

        // Create new order ID
        var lastOrderId = Context.Carts
            .Where(c => c.OrderId != null)
            .Max(c => c.OrderId);

        cart.OrderId = lastOrderId + 1;

        await Context.Carts.AddAsync(cart);
        await SaveAsync();

        // Add purchased items from admin
        foreach (var bookId in orderCreateDto.BookIds)
        {
            var purchaseItemDto = new PurchaseItemCreateDto
            {
                CartId = cart.Id,
                BookId = bookId,
                Count = 1,
            };

            var purchaseItem = PurchaseItemMapper.CreateDtoToEntity(purchaseItemDto);

            await Context.PurchaseItems.AddAsync(purchaseItem);
            await SaveAsync();
        }

        return CartMapper.ToDto(cart);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Cart? cart = await Context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        if (cart == null)
        {
            return false;
        }

        Context.Carts.Remove(cart);
        await SaveAsync();

        return true;
    }

    public async Task<CartDto?> UpdateAsync(int id, CartUpdateDto cartUpdateDto)
    {
        // Validate other values
        await ValidateValues(cartUpdateDto.OrderId, cartUpdateDto.TotalValue);

        Cart? cart = await Context.Carts.FirstOrDefaultAsync(c => c.Id == id);
        if (cart == null)
        {
            return null;
        }

        CartMapper.UpdateEntity(cart, cartUpdateDto);
        await SaveAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task<CartDto?> UpdateOrderAsync(int id, OrderUpdateDto orderUpdateDto)
    {
        // Validate other values
        await ValidateValues(null, orderUpdateDto.TotalValue);

        Cart? cart = await Context.Carts
            .Include(c => c.PurchaseItems)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (cart == null)
        {
            return null;
        }

        CartMapper.UpdateOrderEntity(cart, orderUpdateDto);
        await SaveAsync();

        // Remove purchase items that was removed in admin multiselect

        var selectedBookIds = orderUpdateDto.BookIds.Distinct().ToList();
        var itemsToRemove = cart.PurchaseItems?
            .Where(pi => !selectedBookIds.Contains(pi.BookId))
            .ToList();

        if (itemsToRemove != null && itemsToRemove.Any())
        {
            Context.PurchaseItems.RemoveRange(itemsToRemove);
            await SaveAsync();
        }

        // Add purchased items from admin
        foreach (var bookId in orderUpdateDto.BookIds)
        {
            bool exists = await Context.PurchaseItems
                .AnyAsync(pi => pi.CartId == cart.Id && pi.BookId == bookId);

            if (exists) {
                continue;
            }

            var purchaseItemDto = new PurchaseItemCreateDto
            {
                CartId = cart.Id,
                BookId = bookId,
                Count = 1,
            };

            var purchaseItem = PurchaseItemMapper.CreateDtoToEntity(purchaseItemDto);

            await Context.PurchaseItems.AddAsync(purchaseItem);
            await SaveAsync();
        }

        return CartMapper.ToDto(cart);
    }

    private async Task ValidateRelatedEntitiesExistAsync(int userId)
    {
        // Validate User exists
        var userExists = await Context.Users.AnyAsync(u => u.Id == userId);
        if (!userExists)
        {
            throw new ArgumentException($"Invalid User ID: {userId}");
        }
    }

    private Task ValidateValues(int? orderId, double totalValue)
    {
        // Validate values
        if (orderId is < 0)
        {
            throw new ArgumentException($"Invalid Order ID: {orderId} - Cannot be negative");
        }

        if (totalValue < 0)
        {
            throw new ArgumentException($"Invalid Total Value: {totalValue} - Cannot be negative");
        }

        return Task.CompletedTask;
    }
}
