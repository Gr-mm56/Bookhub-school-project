using BusinessLayer.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Book.Requests;

public class BookSearchDto : PagedRequestDto
{
    [MaxLength(200)]
    public string? Title { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }

    [MaxLength(100)]
    public string? Author { get; set; }

    [MaxLength(50)]
    public string? Genre { get; set; }

    [MaxLength(100)]
    public string? Publisher { get; set; }

    [Range(0.01, double.MaxValue)]
    public double? Price { get; set; }
}
