using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IImageService : ICrudService<ImageDto, ImageDto, ImageRequestDto, ImageRequestDto>
{

}