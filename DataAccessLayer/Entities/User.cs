using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(64)]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64)]
    public required string Surname { get; set; }

    [MaxLength(64)]
    public string Country { get; set; }

    [MaxLength(64)]
    public string City { get; set; }

    [MaxLength(64)]
    public string Street { get; set; }

    public virtual ICollection<Cart>? Carts { get; set; }

    public virtual ICollection<WishlistItem>? WishlistItems { get; set; }

    public virtual ICollection<Rating>? Ratings { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    public virtual Image? ProfilePhoto { get; set; }

    public int? ProfilePhotoId { get; set; }
}
