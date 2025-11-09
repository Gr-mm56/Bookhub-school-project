using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService : ICrudService<PublisherBooksDto, PublisherBooksDto, PublisherRequestDto, PublisherRequestDto>
{

}