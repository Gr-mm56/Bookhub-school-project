using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;
public class Author : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    public virtual Image? ProfilePhoto { get; set; }

    public int ProfilePhotoId { get; set; }

    public virtual ICollection<Book> Books { get; set; }
}