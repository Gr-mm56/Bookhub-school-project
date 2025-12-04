using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Publisher.Responses;
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

