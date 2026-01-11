using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;
public class Publisher : BaseEntity
{
    [Required]
    [MaxLength(100)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(150)]
    public required string Address { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();

    [ForeignKey(nameof(ProfilePhotoId))]
    public virtual Image? ProfilePhoto { get; set; }

    public int? ProfilePhotoId { get; set; }
}
