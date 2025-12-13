using BusinessLayer.Models.Publisher.Responses;
using WebMVC.Models;

namespace WebMVC.Mappers
{
    public static class PublisherMapper
    {
        public static PublisherDetailViewModel ToPublisherDetailViewModel(PublisherBooksDto publisherDto)
        {
            return new PublisherDetailViewModel
            {
                Id = publisherDto.Id,
                Name = publisherDto.Name,
                Address = publisherDto.Address,
                ImageUrl = publisherDto.ProfilePhoto?.FileUrl,
                Books = publisherDto.Books?.Select(BookMapper.ToBookCardViewModel).ToList() ?? []
            };
        }

        public static PublisherCardViewModel ToPublisherCardViewModel(PublisherBooksDto publisherDto)
        {
            return new PublisherCardViewModel
            {
                Id = publisherDto.Id,
                Name = publisherDto.Name,
                ProfilePhotoUrl = publisherDto.ProfilePhoto?.FileUrl,
                BookCount = publisherDto.Books?.Count ?? 0
            };
        }

        public static List<PublisherCardViewModel> ToPublisherCardViewModels(IEnumerable<PublisherBooksDto> publisherDtos)
        {
            return publisherDtos.Select(ToPublisherCardViewModel).ToList();
        }
    }
}