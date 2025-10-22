using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.PurchaseItem.Requests;
using BusinessLayer.Models.PurchaseItem.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class PurchaseItemService : BaseService<BookHubDbContext>, IPurchaseItemService
{
    public PurchaseItemService(BookHubDbContext context): base(context)
    {
    }

    public async Task<PagedResultDto<PurchaseItemDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.PurchaseItems
            .AsNoTracking()
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, PurchaseItemMapper.ToDtoList);
    }

    public async Task<PurchaseItemDto?> GetByIdAsync(int id)
    {
        var purchaseItem = await Context.PurchaseItems
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return purchaseItem != null ? PurchaseItemMapper.ToDto(purchaseItem) : null;

    }

    public async Task<PurchaseItemDto> CreateAsync(PurchaseItemCreateDto purchaseItemCreateDto)
    {
        PurchaseItem purchaseItem = PurchaseItemMapper.CreateDtoToEntity(purchaseItemCreateDto);

        await Context.PurchaseItems.AddAsync(purchaseItem);
        await SaveAsync();

        return PurchaseItemMapper.ToDto(purchaseItem);

    }

    public async Task<bool> DeleteAsync(int id)
    {
        PurchaseItem? purchaseItem = await Context.PurchaseItems.FirstOrDefaultAsync(g => g.Id == id);
        if (purchaseItem == null)
            return false;

        Context.PurchaseItems.Remove(purchaseItem);
        await SaveAsync();
        return true;
    }

    public async Task<PurchaseItemDto?> UpdateAsync(int id, PurchaseItemUpdateDto purchaseItemUpdateDto)
    {
        PurchaseItem? purchaseItem = await Context.PurchaseItems.FirstOrDefaultAsync(u => u.Id == id);
        if (purchaseItem == null)
            return null;

        PurchaseItemMapper.UpdateEntity(purchaseItem, purchaseItemUpdateDto);
        await SaveAsync();

        return PurchaseItemMapper.ToDto(purchaseItem);
    }
}
