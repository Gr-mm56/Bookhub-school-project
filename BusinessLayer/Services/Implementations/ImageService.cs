using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ImageService(BookHubDbContext context) : BaseService<BookHubDbContext>(context), IImageService
{
    public async Task<PagedResultDto<ImageDto>> GetImagesAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Images
            .AsNoTracking()
            .OrderBy(i => i.Id);

        return await PageAsync(query, limit, offset, ImageMapper.ToDtoList);
    }

    public async Task<ImageDto?> GetImageByIdAsync(int id)
    {
        var image = await Context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        return image != null ? ImageMapper.ToDto(image) : null;
    }

    public async Task<ImageRequestDto> CreateImageAsync(ImageRequestDto requestDto)
    {
        var image = ImageMapper.ToEntity(requestDto);

        // Set timestamps
        image.CreatedAt = DateTime.UtcNow;
        image.UpdatedAt = DateTime.UtcNow;

        await Context.Images.AddAsync(image);
        await SaveAsync();

        return requestDto;
    }

    public async Task<ImageRequestDto?> UpdateImageAsync(int id, ImageRequestDto requestDto)
    {
        var image = await Context.Images
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null)
            return null;

        // Update properties
        ImageMapper.UpdateEntity(image, requestDto);

        await SaveAsync();

        return requestDto;
    }

    public async Task<bool?> DeleteImageAsync(int id)
    {
        var image = await Context.Images.FirstOrDefaultAsync(i => i.Id == id);
        if (image == null)
        {
            return false;
        }

        // Check for references before deleting
        var hasAuthorReferences = await Context.Authors.AnyAsync(a => a.ProfilePhotoId == id);
        var hasBookReferences = await Context.Books.AnyAsync(b => b.ImageId == id);
        var hasPublisherReferences = await Context.Publishers.AnyAsync(p => p.ProfilePhotoId == id);
        var hasUserReferences = await Context.Users.AnyAsync(u => u.ProfilePhotoId == id);

        if (hasAuthorReferences || hasBookReferences || hasPublisherReferences || hasUserReferences)
        {
            // Image is referenced by other entities and cannot be deleted
            return null;
        }

        Context.Images.Remove(image);
        await SaveAsync();
        return true;
    }
}
