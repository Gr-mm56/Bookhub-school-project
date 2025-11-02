using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Image : BaseEntity
{
    [Required]
    public required string FileUrl { get; set; }

    public virtual User? User { get; set; }

    public virtual Publisher? Publisher { get; set; }

    public virtual Author? Author { get; set; }
}
