using BusinessLayer.Models.Book.Responses;
using System.Collections.Generic;

namespace BusinessLayer.Models.Genre.Responses;

public class GenreDetailDto : GenreDto
{
    public ICollection<BookDto> Books { get; set; } = new List<BookDto>();
    public ICollection<BookDto> PrimaryBooks { get; set; } = new List<BookDto>();
}