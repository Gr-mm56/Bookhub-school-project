using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Models.Publisher.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IPublisherService
{
    Task<PagedResultDto<PublisherDto>> GetPublishersAsync(int limit, int offset);
    Task<PublisherDto?> GetPublisherByIdAsync(int id);
    Task<PublisherBooksDto?> GetPublisherBooksAsync(int id);
    Task<PublisherBooksDto> CreatePublisherAsync(PublisherRequestDto requestDto);
    Task<PublisherBooksDto?> UpdatePublisherAsync(int id, PublisherRequestDto requestDto);
    Task<bool> DeletePublisherAsync(int id);
}
