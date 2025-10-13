using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class RatingController(IRatingService ratingService) : ControllerBase
{
    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetRatings([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await ratingService.GetRatingsAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchRatings([FromQuery] RatingSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await ratingService.SearchRatingsAsync(searchDto);
        return Ok(result);
    }

    [HttpGet]
    [Route("details/{id:int}")]
    public async Task<IActionResult> GetRatingDetail(int id)
    {
        var rating = await ratingService.GetRatingDetailAsync(id);
        if (rating == null)
        {
            return NotFound();
        }

        return Ok(rating);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRatingById(int id)
    {
        var rating = await ratingService.GetRatingByIdAsync(id);
        if (rating == null)
        {
            return NotFound();
        }

        return Ok(rating);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateRating([FromBody] RatingRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var rating = await ratingService.CreateRatingAsync(requestDto);
            return CreatedAtAction(nameof(GetRatingById), new { id = rating.Id }, rating);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateRating(int id, [FromBody] RatingRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var rating = await ratingService.UpdateRatingAsync(id, requestDto);
            if (rating == null)
            {
                return NotFound();
            }

            return Ok(rating);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteRating(int id)
    {
        var result = await ratingService.DeleteRatingAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}