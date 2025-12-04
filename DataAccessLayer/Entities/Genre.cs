using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;

public class Genre: BaseEntity
{
    [Required]
    [MaxLength(50, ErrorMessage = "The Name cannot exceed 50 characters.")]
    public required string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    public virtual ICollection<Book> PrimaryBooks { get; set; } = new List<Book>();
}
