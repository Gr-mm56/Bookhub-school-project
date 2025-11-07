using System.ComponentModel.DataAnnotations;
using BusinessLayer.Models.Common;

namespace BusinessLayer.Models.Genre.Requests;

public class GenreSearchDto : PagedRequestDto
{
    [Required]
    [MinLength(1)]
    [MaxLength(50)]
    public required string Name { get; set; } = string.Empty;
}
