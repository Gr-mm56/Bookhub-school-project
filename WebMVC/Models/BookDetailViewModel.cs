namespace WebMVC.Models;

public class BookDetailViewModel
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public double Price { get; set; }
    public string? ImageUrl { get; set; }
    public string? Description { get; set; }
    public string? ISBN { get; set; }
    public GenreViewModel? PrimaryGenre { get; set; }
    public List<GenreViewModel> Genres { get; set; } = [];
    public List<AuthorViewModel> Authors { get; set; } = [];
    public PublisherViewModel? Publisher { get; set; }
    public List<RatingViewModel> Ratings { get; set; } = [];
    public bool IsSignedIn { get; set; }
    
    public double AverageRating => Ratings.Any() 
        ? Math.Round(Ratings.Average(r => r.Stars), 1) 
        : 0;
    
    public int RatingCount => Ratings.Count;
}

public class AuthorViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

public class GenreViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class PublisherViewModel
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class RatingViewModel
{
    public int Id { get; set; }
    public int Stars { get; set; }
    public DateTime CreatedAt { get; set; }
}

