using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models;

public class Book: BaseEntity
{
    [Required]
    public string Title { get; set; }
    [Required]
    [MaxLength(17)]
    public string ISBN { get; set; }

    [MaxLength(300)]
    public string? Description { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    // todo: uncomment when Author is done
  //  [Required]
  //  [ForeignKey(nameof(AuthorId))]
  //  public Author?  Author { get; set; }
    
    [Required]
    public required int AuthorId { get; set; }
    
    public virtual IEnumerable<Genre> Genres { get; set; }
    
    public virtual IEnumerable<Rating>? Ratings { get; set; }
    // todo: uncomment when Images are done
    //public virtual IEnumerable<Image> Images { get; set; }
  // todo: uncomment when Publishers are done  
//    public virtual IEnumerable<Publisher>  Publishers { get; set; }
}