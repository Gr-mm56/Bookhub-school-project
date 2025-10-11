using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

public class Genre: BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }

    [JsonIgnore]
    public virtual ICollection<Book> Books { get; set; }
}
