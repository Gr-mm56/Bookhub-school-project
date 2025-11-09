using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Publisher.Requests;

public class PublisherRequestDto
{
    [Required]
    [MaxLength(100, ErrorMessage = "The Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(150, ErrorMessage = "The Adress cannot exceed 150 characters.")]
    public required string Address { get; set; }

    public int? ProfilePhotoId { get; set; }

    public List<int> BookIds { get; set; } = new List<int>();
}