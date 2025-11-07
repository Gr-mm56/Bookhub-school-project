using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IAuthorService : ICrudService<AuthorDto, AuthorBooksDto, AuthorRequestDto, AuthorRequestDto>
{

}