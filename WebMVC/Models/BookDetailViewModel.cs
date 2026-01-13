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

    public List<AuthorDetailViewModel> Authors { get; set; } = [];

    public PublisherDetailViewModel? Publisher { get; set; }

    public List<RatingViewModel> Ratings { get; set; } = [];

    public bool IsSignedIn { get; set; }

    public int? CurrentUserId { get; set; }

    public RatingViewModel? CurrentUserRating => CurrentUserId.HasValue 
        ? Ratings.FirstOrDefault(r => r.UserId == CurrentUserId.Value)
        : null;

    public double AverageRating => Ratings.Any()
        ? Math.Round(Ratings.Average(r => r.Stars), 1)
        : 0;

    public int RatingCount => Ratings.Count;
}
