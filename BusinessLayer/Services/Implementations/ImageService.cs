using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class ImageService : BaseService<BookHubDbContext>, IImageService
{
    public ImageService(BookHubDbContext dbContext) : base(dbContext)
    {

    }

    public async Task<PagedResultDto<ImageDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Images
            .AsNoTracking()
            .OrderBy(i => i.Id);

        return await PageAsync(query, limit, offset, ImageMapper.ToDtoList);
    }

    public async Task<ImageDto?> GetByIdAsync(int id)
    {
        var image = await Context.Images
            .AsNoTracking()
            .FirstOrDefaultAsync(i => i.Id == id);

        return image != null ? ImageMapper.ToDto(image) : null;
    }

    public async Task<ImageDto> CreateAsync(ImageRequestDto requestDto)
    {
        var image = ImageMapper.CreateEntity(requestDto);

        await Context.Images.AddAsync(image);
        await SaveAsync();

        var createdImage = await Context.Images
            .Include(b => b.Author)
            .Include(b => b.Publisher)
            .Include(b => b.User)
            .FirstAsync(p => p.Id == image.Id);

        return ImageMapper.ToDto(createdImage);
    }

    public async Task<ImageDto> UpdateAsync(int id, ImageRequestDto requestDto)
    {
        var image = await Context.Images
            .FirstOrDefaultAsync(i => i.Id == id);

        if (image == null)
        {
            return null;
        }

        // Update properties
        ImageMapper.UpdateEntity(image, requestDto);

        await SaveAsync();

        return ImageMapper.ToDto(image);
    }

    public async Task<bool> DeleteAsync(int id)
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
            return false;
        }

        Context.Images.Remove(image);
        await SaveAsync();
        return true;
    }
}
