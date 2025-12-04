using BusinessLayer.Models.Common;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Book.Requests;

public class BookSearchDto : PagedRequestDto
{
    [MaxLength(200)]
    public string? SearchTerm { get; set; }
    
    [Range(0.01, double.MaxValue)]
    public double? Price { get; set; }
}
