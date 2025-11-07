using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class BookController: BaseController<BookDto, BookDetailDto, BookRequestDto, BookRequestDto, IBookService>
{
    public BookController(IBookService bookService) : base(bookService)
    {

    }

    [HttpGet]
    [Route("search")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SearchBooks([FromQuery] BookSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await Service.SearchBooksAsync(searchDto);
        return Ok(result);
    }
}
