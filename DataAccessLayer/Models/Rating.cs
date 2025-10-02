using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Rating: BaseEntity
{
    [Required]
    [Range(0,5)]
    public int Stars { get; set; }
    /*[Required] // todo: uncomment when user  is done
    [ForeignKey(nameof(UserId))]
    public virtual User User { get; set; }
    public int UserId { get; set; }*/
    [Required]
    [ForeignKey(nameof(BookId))]
    public virtual Book Book { get; set; }
    public int BookId { get; set; }
    [Required]
    public DateTime DateCreated { get; set; }
    [Required]
    public DateTime DateModified { get; set; }
}