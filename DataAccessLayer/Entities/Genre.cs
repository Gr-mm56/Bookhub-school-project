using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Genre: BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }
}