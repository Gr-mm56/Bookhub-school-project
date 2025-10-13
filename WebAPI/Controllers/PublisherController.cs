using BusinessLayer.Models.Common;
using BusinessLayer.Models.Publisher.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class PublisherController(IPublisherService publisherService) : ControllerBase
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetPublishers([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await publisherService.GetPublishersAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPublisherById(int id)
    {
        var publisher = await publisherService.GetPublisherByIdAsync(id);
        if (publisher == null)
        {
            return NotFound();
        }

        return Ok(publisher);
    }

    [HttpGet]
    [Route("books/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetPublisherBooks(int id)
    {
        var publisherBooks = await publisherService.GetPublisherBooksAsync(id);
        if (publisherBooks == null)
        {
            return NotFound();
        }

        return Ok(publisherBooks);
    }

    [HttpPost]
    [Route("")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreatePublisher([FromBody] PublisherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var publisher = await publisherService.CreatePublisherAsync(requestDto);
            return CreatedAtAction(nameof(GetPublisherById), new { id = publisher.Id }, publisher);
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
    public async Task<IActionResult> UpdatePublisher(int id, [FromBody] PublisherRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var publisher = await publisherService.UpdatePublisherAsync(id, requestDto);
            if (publisher == null)
            {
                return NotFound();
            }

            return Ok(publisher);
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
    public async Task<IActionResult> DeletePublisher(int id)
    {
        var result = await publisherService.DeletePublisherAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
