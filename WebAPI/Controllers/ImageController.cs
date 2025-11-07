using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Models.Image.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class ImageController : BaseController<ImageDto, ImageDto, ImageRequestDto, ImageRequestDto, IImageService>
{
    public ImageController(IImageService imageService) : base(imageService)
    {

    }
}
