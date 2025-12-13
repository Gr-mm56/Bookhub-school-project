using BusinessLayer.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Rating.Requests;

public class RatingSearchDto : PagedRequestDto
{
    public int? UserId { get; set; }

    public int? BookId { get; set; }

    [Range(1, 5)]
    public int? MinStars { get; set; }

    [Range(1, 5)]
    public int? MaxStars { get; set; }
}
