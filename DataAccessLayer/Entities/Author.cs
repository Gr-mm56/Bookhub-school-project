using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public class Author : BaseEntity
{
    [Required]
    [MaxLength(30)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(30)]
    public required string Surname { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    public virtual Image? ProfilePhoto { get; set; }

    public int? ProfilePhotoId { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
