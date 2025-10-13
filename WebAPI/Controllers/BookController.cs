using BusinessLayer.Models.Book.Requests;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class BookController(IBookService bookService) : Controller
{
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetBooks([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await bookService.GetBooksAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetBookDetail(int id)
    {
        var book = await bookService.GetBookDetailAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        return Ok(book);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] BookSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await bookService.SearchBooksAsync(searchDto);
        return Ok(result);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateBook([FromBody] BookRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var book = await bookService.CreateBookAsync(requestDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.Id }, book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var book = await bookService.UpdateBookAsync(id, requestDto);
            if (book == null)
            {
                return NotFound();
            }

            return Ok(book);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await bookService.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}