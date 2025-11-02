using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Cart.Requests;
using BusinessLayer.Models.Cart.Responses;
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
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, CartMapper.ToDtoList);
    }

    public async Task<CartDetailDto?> GetByIdAsync(int id)
    {
        var cart = await Context.Carts
            .Include(c => c.User)
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return cart != null ? CartMapper.ToDetailDto(cart) : null;
    }

    public async Task<CartDto> CreateAsync(CartCreateDto cartCreateDto)
    {
        // Validate that User exists
        await ValidateRelatedEntitiesExistAsync(cartCreateDto);

        // Validate other values
        await ValidateValues(cartCreateDto.OrderId, cartCreateDto.TotalValue);

        Cart cart = CartMapper.CreateDtoToEntity(cartCreateDto);

        await Context.Carts.AddAsync(cart);
        await SaveAsync();

        return CartMapper.ToDto(cart);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        Cart? cart = await Context.Carts.FirstOrDefaultAsync(g => g.Id == id);
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

        Cart? cart = await Context.Carts.FirstOrDefaultAsync(u => u.Id == id);
        if (cart == null)
        {
            return null;
        }

        CartMapper.UpdateEntity(cart, cartUpdateDto);
        await SaveAsync();

        return CartMapper.ToDto(cart);
    }

    private async Task ValidateRelatedEntitiesExistAsync(CartCreateDto cartDto)
    {
        // Validate User exists
        var userExists = await Context.Users.AnyAsync(u => u.Id == cartDto.UserId);
        if (!userExists)
        {
            throw new ArgumentException($"Invalid User ID: {cartDto.UserId}");
        }
    }

    private async Task ValidateValues(int? orderId, double totalValue)
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
    }
}
