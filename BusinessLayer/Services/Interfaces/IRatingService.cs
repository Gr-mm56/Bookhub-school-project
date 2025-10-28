using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IRatingService : ICrudService<RatingDto, RatingDetailDto, RatingRequestDto, RatingRequestDto>
{
    Task<RatingDetailDto?> GetRatingDetailAsync(int id);

    Task<PagedResultDto<RatingDto>> SearchRatingsAsync(RatingSearchDto searchDto);
}
