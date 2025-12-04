using BusinessLayer.Models.Book.Responses;
using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Author.Requests;

public class AuthorRequestDto
{
    [Required]
    [MaxLength(30, ErrorMessage = "The Name cannot exceed 30 characters.")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(30, ErrorMessage = "The Surname cannot exceed 30 characters.")]
    public required string Surname { get; set; }

    public int? ProfilePhotoId { get; set; }

    public List<int> BookIds { get; set; } = new List<int>();

}