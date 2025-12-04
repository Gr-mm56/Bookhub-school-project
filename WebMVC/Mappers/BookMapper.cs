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

        public static BookDetailViewModel ToBookDetailViewModel(BookDetailDto bookDetailDto)
        {
            return new BookDetailViewModel
            {
                Id = bookDetailDto.Id,
                Title = bookDetailDto.Title,
                Price = bookDetailDto.Price,
                ImageUrl = bookDetailDto.Image?.FileUrl,
                Description = bookDetailDto.Description,
                ISBN = bookDetailDto.ISBN,
                PrimaryGenre = bookDetailDto.PrimaryGenre != null 
                    ? new GenreViewModel 
                    { 
                        Id = bookDetailDto.PrimaryGenre.Id,
                        Name = bookDetailDto.PrimaryGenre.Name 
                    }
                    : null,
                Genres = bookDetailDto.Genres.Select(g => new GenreViewModel 
                { 
                    Id = g.Id, 
                    Name = g.Name 
                }).ToList(),
                Authors = bookDetailDto.Authors.Select(a => new AuthorViewModel 
                { 
                    Id = a.Id, 
                    Name = a.Name, 
                    Surname = a.Surname 
                }).ToList(),
                Publisher = bookDetailDto.Publisher != null 
                    ? new PublisherViewModel 
                    { 
                        Id = bookDetailDto.Publisher.Id, 
                        Name = bookDetailDto.Publisher.Name 
                    }
                    : null,
                Ratings = bookDetailDto.Ratings.Select(r => new RatingViewModel 
                { 
                    Id = r.Id, 
                    Stars = r.Stars, 
                    CreatedAt = r.CreatedAt 
                }).ToList()
            };
        }
    }
}

