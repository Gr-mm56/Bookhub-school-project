using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class BookGenre
{
    [Required]
    public int BookId { get; set; }
    public virtual Book Book { get; set; }
    [Required]
    public int GenreId { get; set; }
    public virtual Genre Genre { get; set; }
}