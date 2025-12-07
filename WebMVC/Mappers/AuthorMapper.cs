using BusinessLayer.Models.Author.Responses;
using WebMVC.Models;

namespace WebMVC.Mappers
{
    public static class AuthorMapper
    {
        public static AuthorDetailViewModel ToAuthorDetailViewModel(AuthorBooksDto authorDto)
        {
            return new AuthorDetailViewModel
            {
                Id = authorDto.Id,
                Name = authorDto.Name,
                Surname = authorDto.Surname,
                ImageUrl = authorDto.ProfilePhoto?.FileUrl,
                Books = authorDto.Books?.Select(b => new BookCardViewModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    Price = b.Price,
                    ImageUrl = b.Image?.FileUrl
                }).ToList() ?? []
            };
        }
        public static AuthorCardViewModel ToAuthorCardViewModel(AuthorBooksDto authorDto)
        {
            return new AuthorCardViewModel
            {
                Id = authorDto.Id,
                Name = authorDto.Name,
                Surname = authorDto.Surname,
                ProfilePhotoUrl = authorDto.ProfilePhoto?.FileUrl,
                BookCount = authorDto.Books?.Count ?? 0
            };
        }

        public static List<AuthorCardViewModel> ToAuthorCardViewModels(IEnumerable<AuthorBooksDto> authorDtos)
        {
            return authorDtos.Select(ToAuthorCardViewModel).ToList();
        }
    }
}