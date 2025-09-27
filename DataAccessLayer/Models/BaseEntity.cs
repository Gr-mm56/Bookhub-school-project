using System.ComponentModel.DataAnnotations;

namespace DataAccessLayer.Models
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
    }
}