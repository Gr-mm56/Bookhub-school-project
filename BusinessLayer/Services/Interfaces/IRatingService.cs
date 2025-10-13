using BusinessLayer.Models.Common;
using BusinessLayer.Models.Rating.Requests;
using BusinessLayer.Models.Rating.Responses;

namespace BusinessLayer.Services.Interfaces;

public interface IRatingService
{
    Task<PagedResultDto<RatingDto>> GetRatingsAsync(int limit = 20, int offset = 0);
    Task<RatingDto?> GetRatingByIdAsync(int id);
    Task<RatingDetailDto?> GetRatingDetailAsync(int id);
    Task<PagedResultDto<RatingDto>> SearchRatingsAsync(RatingSearchDto searchDto);
    Task<RatingDto> CreateRatingAsync(RatingRequestDto requestDto);
    Task<RatingDto?> UpdateRatingAsync(int id, RatingRequestDto requestDto);
    Task<bool> DeleteRatingAsync(int id);
}