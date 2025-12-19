using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    [Required]
    [MaxLength(150)]
    public required string Title { get; set; }

    [Required]
    [MaxLength(17)]
    public required string ISBN { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    [Range(0.0, double.MaxValue)]
    public required double Price { get; set; }

    [Required]
    public required int PrimaryGenreId { get; set; }

    [ForeignKey(nameof(PrimaryGenreId))]
    public virtual Genre? PrimaryGenre { get; set; }

    public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();

    public virtual ICollection<Rating> Ratings { get; set; } = new List<Rating>();

    public virtual ICollection<Author> Authors { get; set; } = new List<Author>();

    [ForeignKey(nameof(PublisherId))]
    public virtual Publisher? Publisher { get; set; }

    public int? PublisherId { get; set; }

    [ForeignKey(nameof(ImageId))]
    public virtual Image? Image { get; set; }

    public int? ImageId { get; set; }
}
