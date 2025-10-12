using BusinessLayer.Models.Common;

namespace BusinessLayer.Models.Genre.Requests;

public class GenreSearchDto : PagedRequestDto
{
    public string? Name { get; set; }
}
