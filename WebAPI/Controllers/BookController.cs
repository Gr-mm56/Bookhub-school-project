using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Book.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController: BaseController<BookDto, BookRequestDto, BookRequestDto, IBookService>
{
    public BookController(IBookService bookService) : base(bookService)
    {
        
    }
    
    [HttpGet]
    [Route("allDetails/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetBookDetail(int id)
    {
        var book = await Service.GetBookDetailAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
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