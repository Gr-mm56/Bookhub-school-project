using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Entities;
public class Image : BaseEntity
{
    [Required]
    public string FileUrl { get; set; }
}
