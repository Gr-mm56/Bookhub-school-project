using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    [Required]
    public string Title { get; set; }

    [Required]
    [MaxLength(17)]
    public string ISBN { get; set; }


    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    public double Price { get; set; }

    public virtual ICollection<Genre> Genres { get; set; }

    public virtual ICollection<Rating>? Ratings { get; set; }

    public virtual ICollection<Author>? Authors { get; set; }

    public virtual ICollection<Publisher>? Publishers { get; set; }


    [ForeignKey(nameof(ImageId))]
    public virtual Image? Image { get; set; }

    public int ImageId { get; set; }
}
