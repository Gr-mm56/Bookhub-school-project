using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class Rating : BaseEntity
{
    [Required]
    [Range(0, 5)] public int Stars { get; set; }

    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }

    public int UserId { get; set; }

    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }

    public int BookId { get; set; }
}
