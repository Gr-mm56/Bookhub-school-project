using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using WebMVC.Areas.Admin.Models.Image;

namespace WebMVC.Areas.Admin.Mappers;

public static class ImageViewModelMapper
{
    public static List<ImageListItemViewModel> ToListViewModel(List<ImageDto> imageDtos)
    {
        return imageDtos.Select(img => new ImageListItemViewModel
        {
            Id = img.Id,
            FileUrl = img.FileUrl,
            CreatedAt = img.CreatedAt
        }).ToList();
    }


    public static ImageListItemViewModel ToListItemViewModel(ImageDto imageDto)
    {
        return new ImageListItemViewModel
        {
            Id = imageDto.Id,
            FileUrl = imageDto.FileUrl,
            CreatedAt = imageDto.CreatedAt
        };
    }

    public static ImageDeleteViewModel ToDeleteViewModel(ImageDto imageDto)
    {
        return new ImageDeleteViewModel
        {
            Id = imageDto.Id,
            FileUrl = imageDto.FileUrl
        };
    }
}

