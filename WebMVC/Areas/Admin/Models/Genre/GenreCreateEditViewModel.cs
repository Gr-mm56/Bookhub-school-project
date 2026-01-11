using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Genre;

public class GenreCreateEditViewModel
{
    [Required(ErrorMessage = "Genre name is required")]
    [MaxLength(50, ErrorMessage = "Genre name cannot exceed 50 characters")]
    public required string Name { get; set; }
}

