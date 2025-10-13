using System.ComponentModel.DataAnnotations;

namespace BusinessLayer.Models.Publisher.Requests;

public class PublisherRequestDto
{
    public string Name { get; set; }

    public string Address { get; set; }

    public int ProfilePhotoId { get; set; }

    public List<int> BookIds { get; set; } = new List<int>();
}