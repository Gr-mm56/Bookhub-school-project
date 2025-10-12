using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

// todo: On Put, updatedAt should be updated, implicitly
[Route("[controller]")]
[ApiController]
public class RatingController : BaseController<Rating>
{
    private readonly IRatingRepository _ratingRepository;

    public RatingController(IRatingRepository ratingRepository) : base(ratingRepository)
    {
        _ratingRepository = ratingRepository;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> GetRatings(
        [FromQuery] int? userId,
        [FromQuery] int? bookId,
        [FromQuery] int? minStars,
        [FromQuery] int? maxStars,
        [FromQuery] int limit = 20,
        [FromQuery] int offset = 0)
    {
        var ratings = await _ratingRepository.GetRatingsAsync(userId, bookId, minStars, maxStars, limit, offset);

        var result = ratings.Select(r => new
        {
            r.Id,
            r.Stars,
            r.UserId,
            r.BookId,
            User = new { r.User.Id, r.User.Name },
            Book = new { r.Book.Id, r.Book.Title }
        });
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetRating(int id) => await Get(id);

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateRating([FromBody] Rating rating) => await Insert(rating);

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateRating(int id, [FromBody] Rating updatedRating) =>
        await Update(id, updatedRating);

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteRating(int id) => await Delete(id);
}