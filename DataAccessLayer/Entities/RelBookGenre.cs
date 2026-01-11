using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class RelBookGenre
{
    public int GenreId { get; set; }

    [ForeignKey(nameof(GenreId))]
    public virtual Genre? Genre { get; set; }

    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }
}
