using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.GiftCard.Requests;
using BusinessLayer.Models.GiftCard.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BusinessLayer.Services.Implementations;

public class GiftCardService : BaseService<BookHubDbContext>, IGiftCardService
{
    private const string CouponPrefix = "GIFT";
    private const string AllowedChars = "ABCDEFGHJKLMNPQRSTUVWXYZ23456789";

    public GiftCardService(BookHubDbContext context) : base(context)
    {
    }

    public async Task<PagedResultDto<GiftCardCouponsDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.GiftCards
            .AsNoTracking()
            .Include(gc => gc.Coupons)
            .OrderByDescending(gc => gc.CreatedAt);

        return await PageAsync(query, limit, offset, GiftCardMapper.ToDetailDtoList);
    }

    public async Task<GiftCardCouponsDto?> GetByIdAsync(int id)
    {
        var giftCard = await Context.GiftCards
            .AsNoTracking()
            .Include(gc => gc.Coupons)
            .FirstOrDefaultAsync(gc => gc.Id == id);

        return giftCard != null ? GiftCardMapper.ToDetailDto(giftCard) : null;
    }

    public async Task<GiftCardCouponsDto> CreateAsync(GiftCardCreateDto createDto)
    {
        ValidateGiftCard(createDto.ValidFrom, createDto.ValidTo, createDto.PriceReduction);

        var giftCard = GiftCardMapper.CreateEntity(createDto);
        await Context.GiftCards.AddAsync(giftCard);
        await SaveAsync();

        var couponCodes = await GenerateUniqueCouponCodesAsync(createDto.NumberOfCoupons);

        foreach (var code in couponCodes)
        {
            var coupon = GiftCardCouponMapper.CreateEntity(code, giftCard.Id);
            await Context.GiftCardCoupons.AddAsync(coupon);
        }

        await SaveAsync();

        return await GetByIdAsync(giftCard.Id)
            ?? throw new InvalidOperationException("Failed to retrieve created gift card.");
    }

    public async Task<GiftCardCouponsDto?> UpdateAsync(int id, GiftCardUpdateDto updateDto)
    {
        ValidateGiftCard(updateDto.ValidFrom, updateDto.ValidTo, updateDto.PriceReduction);

        var giftCard = await Context.GiftCards
            .Include(gc => gc.Coupons)
            .FirstOrDefaultAsync(gc => gc.Id == id);

        if (giftCard == null)
        {
            return null;
        }

        GiftCardMapper.UpdateEntity(giftCard, updateDto);

        if (updateDto.AdditionalCoupons > 0)
        {
            var newCouponCodes = await GenerateUniqueCouponCodesAsync(updateDto.AdditionalCoupons);

            foreach (var code in newCouponCodes)
            {
                var coupon = GiftCardCouponMapper.CreateEntity(code, giftCard.Id);
                await Context.GiftCardCoupons.AddAsync(coupon);
            }
        }

        await SaveAsync();

        return await GetByIdAsync(id);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var giftCard = await Context.GiftCards
            .Include(gc => gc.Coupons)
            .FirstOrDefaultAsync(gc => gc.Id == id);

        if (giftCard == null)
        {
            return false;
        }

        if (giftCard.Coupons.Any(c => c.UsedInOrderId.HasValue))
        {
            throw new InvalidOperationException("Cannot delete a gift card that has coupons already used in orders.");
        }

        Context.GiftCards.Remove(giftCard);
        await SaveAsync();

        return true;
    }

    private async Task<List<string>> GenerateUniqueCouponCodesAsync(int count)
    {
        var codes = new HashSet<string>(count);
        var maxAttempts = count * 10;
        var attempts = 0;

        while (codes.Count < count && attempts < maxAttempts)
        {
            var code = GenerateCouponCode();

            if (!await Context.GiftCardCoupons.AnyAsync(c => c.Code == code))
            {
                codes.Add(code);
            }

            attempts++;
        }

        if (codes.Count < count)
        {
            throw new InvalidOperationException(
                $"Failed to generate {count} unique coupon codes. Only generated {codes.Count}.");
        }

        return codes.ToList();
    }

    private static string GenerateCouponCode()
    {
        // Format: GIFT-XXXX-XXXX-XXXX
        Span<char> buffer = stackalloc char[19];
        int pos = 0;

        // Add prefix
        CouponPrefix.AsSpan().CopyTo(buffer);
        pos += CouponPrefix.Length;

        // Add three groups of 4 random characters
        for (int group = 0; group < 3; group++)
        {
            buffer[pos++] = '-';

            for (int i = 0; i < 4; i++)
            {
                buffer[pos++] = AllowedChars[RandomNumberGenerator.GetInt32(AllowedChars.Length)];
            }
        }

        return new string(buffer);
    }

    private static void ValidateGiftCard(DateTime validFrom, DateTime validTo, double priceReduction)
    {
        if (validFrom >= validTo)
        {
            throw new ArgumentException("ValidFrom must be before ValidTo.");
        }

        if (priceReduction <= 0)
        {
            throw new ArgumentException("PriceReduction must be greater than zero.");
        }
    }
}
