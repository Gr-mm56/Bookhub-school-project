using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;
public class Author : BaseEntity
{
    [Required]
    public string Name { get; set; }

    [Required]
    public string Surname { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    [JsonIgnore]
    public virtual Image? ProfilePhoto { get; set; }

    public int ProfilePhotoId { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; }
}
