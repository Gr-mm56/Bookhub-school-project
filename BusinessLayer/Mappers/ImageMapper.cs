using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using DataAccessLayer.Entities;

namespace BusinessLayer.Mappers;

public static class ImageMapper
{
    public static ImageDto ToDto(Image image)
    {
        ArgumentNullException.ThrowIfNull(image);

        return new ImageDto
        {
            Id = image.Id,
            FileUrl = image.FileUrl
        };
    }

    public static Image ToEntity(ImageRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(requestDto);

        return new Image
        {
            FileUrl = requestDto.FileUrl,
        };
    }

    public static void UpdateEntity(Image image, ImageRequestDto requestDto)
    {
        ArgumentNullException.ThrowIfNull(image);
        ArgumentNullException.ThrowIfNull(requestDto);

        image.FileUrl = requestDto.FileUrl;
        image.UpdatedAt = DateTime.UtcNow;
    }

    public static IEnumerable<ImageDto> ToDtoList(IEnumerable<Image> images)
    {
        return images.Select(ToDto);
    }
}