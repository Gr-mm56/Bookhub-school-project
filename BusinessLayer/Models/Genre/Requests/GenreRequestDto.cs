using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Genre.Requests;

public class GenreRequestDto
{
    [MaxLength(50)]
    public required string Name { get; set; }
}
