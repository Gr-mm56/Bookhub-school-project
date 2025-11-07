using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IImageService
{
    Task<PagedResultDto<ImageDto>> GetImagesAsync(int limit, int offset);
    Task<ImageDto?> GetImageByIdAsync(int id);
    Task<ImageRequestDto> CreateImageAsync(ImageRequestDto requestDto);
    Task<ImageRequestDto?> UpdateImageAsync(int id, ImageRequestDto requestDto);
    Task<bool?> DeleteImageAsync(int id);
}
