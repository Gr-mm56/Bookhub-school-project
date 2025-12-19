using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;
public class RelBookAuthor
{
    public int AuthorId { get; set; }

    [ForeignKey(nameof(AuthorId))]
    public virtual Author Author { get; set; }

    public int BookId { get; set; }

    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }
}
