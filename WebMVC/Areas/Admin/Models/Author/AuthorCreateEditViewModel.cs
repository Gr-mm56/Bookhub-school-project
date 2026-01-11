using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Author;

public class AuthorCreateEditViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Surname is required")]
    [MaxLength(30, ErrorMessage = "Surname cannot exceed 30 characters")]
    public required string Surname { get; set; }

    public int? ProfilePhotoId { get; set; }

    [MinLength(1, ErrorMessage = "At least one book must be selected")]
    public List<int> BookIds { get; set; } = [];
}