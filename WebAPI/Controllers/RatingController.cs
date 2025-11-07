using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public class RatingController : BaseController<RatingDto, RatingDetailDto, RatingRequestDto, RatingRequestDto, IRatingService>
{
    public RatingController(IRatingService service): base(service)
    {

    }

    [HttpGet]
    [Route("search")]
    public async Task<IActionResult> SearchRatings([FromQuery] RatingSearchDto searchDto)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await Service.SearchRatingsAsync(searchDto);
        return Ok(result);
    }
}
