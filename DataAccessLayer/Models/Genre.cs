using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models;

public class Genre: BaseEntity
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; }
}