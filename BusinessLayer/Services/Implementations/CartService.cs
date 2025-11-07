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

    public async Task<PagedResultDto<CartDto>> GetCartsAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Carts
            .AsNoTracking()
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, CartMapper.ToDtoList);
    }

    public async Task<CartDto?> GetCartByIdAsync(int id)
    {
        var cart = await Context.Carts
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return cart != null ? CartMapper.ToDto(cart) : null;

    }

    public async Task<CartDto> CreateCartAsync(CartCreateDto cartCreateDto)
    {
        Cart cart = CartMapper.CreateDtoToEntity(cartCreateDto);

        await Context.Carts.AddAsync(cart);
        await SaveAsync();

        return CartMapper.ToDto(cart);

    }

    public async Task<bool> DeleteCartAsync(int id)
    {
        Cart? cart = await Context.Carts.FirstOrDefaultAsync(g => g.Id == id);
        if (cart == null)
            return false;

        Context.Carts.Remove(cart);
        await SaveAsync();
        return true;
    }

    public async Task<CartDto?> UpdateCartAsync(int id, CartUpdateDto cartUpdateDto)
    {
        Cart? cart = await Context.Carts.FirstOrDefaultAsync(u => u.Id == id);
        if (cart == null)
            return null;

        CartMapper.UpdateEntity(cart, cartUpdateDto);
        await SaveAsync();

        return CartMapper.ToDto(cart);
    }
}
