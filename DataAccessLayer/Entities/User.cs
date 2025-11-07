using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

public class User : BaseEntity
{
    [Required]
    [MaxLength(64, ErrorMessage = "The Name cannot exceed 64 characters.")]
    public required string Name { get; set; }

    [Required]
    [MaxLength(64, ErrorMessage = "The Surname cannot exceed 64 characters.")]
    public required string Surname { get; set; }

    [MaxLength(64, ErrorMessage = "The Country cannot exceed 64 characters.")]
    public string Country { get; set; }

    [MaxLength(64, ErrorMessage = "The City cannot exceed 64 characters.")]
    public string City { get; set; }

    [MaxLength(64, ErrorMessage = "The Street cannot exceed 64 characters.")]
    public string Street { get; set; }

    [JsonIgnore]
    public virtual ICollection<Cart>? Carts { get; set; }

    [JsonIgnore]
    public virtual ICollection<WishlistItem>? WishlistItems { get; set; }

    [JsonIgnore]
    public virtual ICollection<Rating>? Ratings { get; set; }

    [ForeignKey(nameof(ProfilePhotoId))]
    [JsonIgnore]
    public virtual Image? ProfilePhoto { get; set; }

    public int? ProfilePhotoId { get; set; }
}
