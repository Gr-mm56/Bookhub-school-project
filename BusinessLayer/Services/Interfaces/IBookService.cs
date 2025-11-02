using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface IBookService : ICrudService<BookDto, BookDetailDto, BookRequestDto, BookRequestDto>
{
    Task<PagedResultDto<BookDetailDto>> SearchBooksAsync(BookSearchDto searchDto);
}
