using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(150, ErrorMessage = "The Title cannot exceed 150 characters.")]
    public required string Title { get; set; }

    [Required]
    [MaxLength(17)]
    [RegularExpression(@"^(?:ISBN(?:-1[03])?:? )?(?=[0-9X]{10}$|(?=(?:[0-9]+[- ]){3})[- 0-9X]{13}$|97[89][0-9]{10}$|(?=(?:[0-9]+[- ]){4})[- 0-9]{17}$)(?:97[89][- ]?)?[0-9]{1,5}[- ]?[0-9]+(?:[- ]?[0-9]+){2}[- ]?[0-9X]$", ErrorMessage = "The ISBN must be a valid ISBN-10 or ISBN-13 format.")]
    public required string ISBN { get; set; }


    [MaxLength(300, ErrorMessage = "Description cannot exceed 300 characters.")]
    public string? Description { get; set; }

    [Required]
    [Range(0.0, double.MaxValue, ErrorMessage = "The Price must be non-negative.")]
    public required double Price { get; set; }

    public virtual ICollection<Genre> Genres { get; set; }

    public virtual ICollection<Rating>? Ratings { get; set; }

    public virtual ICollection<Author> Authors { get; set; }

    [ForeignKey(nameof(PublisherId))]
    public virtual Publisher? Publisher { get; set; }
    public int? PublisherId { get; set; }


    [ForeignKey(nameof(ImageId))]
    public virtual Image? Image { get; set; }

    public int? ImageId { get; set; }
}
