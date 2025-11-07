using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class PublisherController : BaseController<PublisherDto, PublisherBooksDto, PublisherRequestDto, PublisherRequestDto, IPublisherService>
{
    public PublisherController(IPublisherService publisherService) : base(publisherService)
    {

    }
}
