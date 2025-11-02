using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService : ICrudService<PublisherDto, PublisherBooksDto, PublisherRequestDto, PublisherRequestDto>
{

}