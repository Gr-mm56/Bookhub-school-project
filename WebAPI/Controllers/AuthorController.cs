using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Author.Responses;
using BusinessLayer.Services.Interfaces;

namespace WebAPI.Controllers;

public class AuthorController : BaseController<AuthorBooksDto, AuthorBooksDto, AuthorRequestDto, AuthorRequestDto, IAuthorService>
{
    public AuthorController(IAuthorService authorService) : base(authorService)
    {

    }
}