using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface IBookService
{
    Task<PagedResultDto<BookDto>> GetBooksAsync(int limit, int offset);
    Task<BookDto?> GetBookByIdAsync(int id);
    Task<BookDetailDto?> GetBookDetailAsync(int id);
    Task<PagedResultDto<BookDetailDto>> SearchBooksAsync(BookSearchDto searchDto);
    Task<BookDetailDto> CreateBookAsync(BookRequestDto requestDto);
    Task<BookDetailDto?> UpdateBookAsync(int id, BookRequestDto requestDto);
    Task<bool> DeleteBookAsync(int id);
}