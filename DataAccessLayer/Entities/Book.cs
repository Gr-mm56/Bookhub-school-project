using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace DataAccessLayer.Entities;

public class Book : BaseEntity
{
    [Required]
    public string Title { get; set; }

    [Required]
    [MaxLength(17)]
    public string ISBN { get; set; }


    [MaxLength(300)]
    public string? Description { get; set; }

    [Required]
    public double Price { get; set; }

    [JsonIgnore]
    public virtual ICollection<Genre> Genres { get; set; }

    [JsonIgnore]
    public virtual ICollection<Rating>? Ratings { get; set; }

    [JsonIgnore]
    public virtual ICollection<Author>? Authors { get; set; }

    [JsonIgnore]
    public virtual ICollection<Publisher>? Publishers { get; set; }


    [ForeignKey(nameof(ImageId))]
    [JsonIgnore]
    public virtual Image? Image { get; set; }

    public int ImageId { get; set; }
}
