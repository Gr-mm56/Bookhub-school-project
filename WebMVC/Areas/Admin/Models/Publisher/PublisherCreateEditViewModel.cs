using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Publisher;

public class PublisherCreateEditViewModel
{
    [Required(ErrorMessage = "Name is required")]
    [MaxLength(100, ErrorMessage = "Name cannot exceed 100 characters")]
    public required string Name { get; set; }

    [Required(ErrorMessage = "Address is required")]
    [MaxLength(150, ErrorMessage = "Address cannot exceed 150 characters")]
    public required string Address { get; set; }

    public int? ProfilePhotoId { get; set; }

    [MinLength(1, ErrorMessage = "At least one book must be selected")]
    public List<int> BookIds { get; set; } = [];
}