using System.ComponentModel.DataAnnotations;

namespace WebMVC.Areas.Admin.Models.Image;

public class ImageCreateEditViewModel
{
    [Required(ErrorMessage = "Please select a file to upload")]
    [Display(Name = "Image File")]
    public IFormFile? File { get; set; }
}

