using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public class Publisher : BaseEntity
{
    [Required]
    [MaxLength(100, ErrorMessage = "The Name cannot exceed 100 characters.")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(150, ErrorMessage = "The Adress cannot exceed 150 characters.")]
    public required string Address { get; set; }

    public virtual ICollection<Book> Books { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    public virtual Image? ProfilePhoto { get; set; }

    public int? ProfilePhotoId { get; set; }

}
