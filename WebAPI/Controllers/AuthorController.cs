using BusinessLayer.Models.Author.Requests;
using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class AuthorController(IAuthorService authorService) : ControllerBase
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAuthors([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await authorService.GetAuthorsAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorById(int id)
    {
        var author = await authorService.GetAuthorByIdAsync(id);
        if (author == null)
        {
            return NotFound();
        }

        return Ok(author);
    }

    [HttpGet]
    [Route("books/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAuthorBooks(int id)
    {
        var authorBooks = await authorService.GetAuthorBooksAsync(id);
        if (authorBooks == null)
        {
            return NotFound();
        }

        return Ok(authorBooks);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAuthor([FromBody] AuthorRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var author = await authorService.CreateAuthorAsync(requestDto);
            return CreatedAtAction(nameof(GetAuthorById), new { id = author.Id }, author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateAuthor(int id, [FromBody] AuthorRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var author = await authorService.UpdateAuthorAsync(id, requestDto);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await authorService.DeleteAuthorAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}