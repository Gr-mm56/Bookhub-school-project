using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Book;

public class BookCreateEditViewModel
{
    [Required(ErrorMessage = "Title is required")]
    [MaxLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
    public required string Title { get; set; }

    [Required(ErrorMessage = "ISBN is required")]
    [MaxLength(17, ErrorMessage = "ISBN cannot exceed 17 characters")]
    public required string ISBN { get; set; }

    [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public required double Price { get; set; }

    [Required(ErrorMessage = "Primary Genre is required")]
    public required int PrimaryGenreId { get; set; }

    public int? ImageId { get; set; }

    public int? PublisherId { get; set; }

    [MinLength(1, ErrorMessage = "At least one genre must be selected")]
    public List<int> GenreIds { get; set; } = [];

    [MinLength(1, ErrorMessage = "At least one author must be selected")]
    public List<int> AuthorIds { get; set; } = [];
}

