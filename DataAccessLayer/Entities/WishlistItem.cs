using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class WishlistItem : BaseEntity
{
    public int UserId { get; set; }

    [Required]
    [ForeignKey(nameof(UserId))]
    public virtual User? User { get; set; }

    public int BookId { get; set; }

    [Required]
    [ForeignKey(nameof(BookId))]
    public virtual Book? Book { get; set; }
}
