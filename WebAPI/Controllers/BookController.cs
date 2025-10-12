using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers;

[Route("[controller]")]
public class BookController : BaseController<Book>
{
    private readonly IBookRepository _bookRepository;

    public BookController(IBookRepository bookRepository) : base(bookRepository)
    {
        _bookRepository = bookRepository;
    }

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetBooks() => await GetAll();

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetBookById(int id) => await Get(id);

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchBooks([FromQuery] string? title, [FromQuery] string? description,
        [FromQuery] string? author, [FromQuery] string? genre, [FromQuery] string? publisher,
        [FromQuery] double? price)
    {
        var books = await _bookRepository.SearchBooksAsync(title, description, author, genre, publisher, price);
        return Ok(books);
    }

    [HttpPost]
    public async Task<IActionResult> CreateBook([FromBody] Book newBook) => await Insert(newBook);

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook) => await Update(id, updatedBook);

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteBook(int id) => await Delete(id);
}