using BusinessLayer.Models.Book.Responses;

namespace BusinessLayer.Models.Author.Requests;

public class AuthorRequestDto
{
    public string Name { get; set; }

    public string Surname { get; set; }

    public int ProfilePhotoId { get; set; }

    public List<int> BookIds { get; set; } = new List<int>();

}