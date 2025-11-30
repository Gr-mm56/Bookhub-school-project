using BusinessLayer.Models.Book.Responses;
using WebMVC.Models;

namespace WebMVC.Mappers
{
    public static class BookMapper
    {

        public static BookCardViewModel ToBookCardViewModel(BookDetailDto bookDetailDto)
        {
            return new BookCardViewModel
            {
                Id = bookDetailDto.Id,
                Title = bookDetailDto.Title,
                Price = bookDetailDto.Price,
                ImageUrl = bookDetailDto.Image?.FileUrl,
                FirstAuthorName = bookDetailDto.Authors.FirstOrDefault() is { } author
                    ? $"{author.Name} {author.Surname}"
                    : null
            };
        }
        
        public static List<BookCardViewModel> ToBookCardViewModels(IEnumerable<BookDetailDto> bookDetailDtos)
        {
            return bookDetailDtos.Select(ToBookCardViewModel).ToList();
        }
    }
}

