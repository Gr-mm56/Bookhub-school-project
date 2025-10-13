using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService
{
    Task<PagedResultDto<AuthorDto>> GetAuthorsAsync(int limit, int offset);
    Task<AuthorDto?> GetAuthorByIdAsync(int id);
    Task<AuthorBooksDto?> GetAuthorBooksAsync(int id);
    Task<AuthorBooksDto> CreateAuthorAsync(AuthorRequestDto requestDto);
    Task<AuthorBooksDto?> UpdateAuthorAsync(int id, AuthorRequestDto requestDto);
    Task<bool> DeleteAuthorAsync(int id);
}