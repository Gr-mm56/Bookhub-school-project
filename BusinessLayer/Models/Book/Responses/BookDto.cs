namespace BusinessLayer.Models.Book.Responses;

public class BookDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime PublishedDate { get; set; }
    public string ISBN { get; set; }
    public double Price { get; set; }
    public int AuthorId { get; set; }
    public string AuthorName { get; set; }
    public int PublisherId { get; set; }
    public string PublisherName { get; set; }
    public int GenreId { get; set; }
    public string GenreName { get; set; }
    public string ImageUrl { get; set; }
}