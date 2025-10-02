using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public static class DataInitializer
{
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var genres = PrepareGenreModels();
        modelBuilder.Entity<Genre>().HasData(genres);
        var books = PrepareBookModels();
        modelBuilder.Entity<Book>().HasData(books);
        var ratings = PrepareRatingModels();
        modelBuilder.Entity<Rating>().HasData(ratings);
    }

    private static List<Genre> PrepareGenreModels()
    {
        return
        [
            new Genre { Id = 1, Name = "Science Fiction" },
            new Genre { Id = 2, Name = "Fantasy" },
            new Genre { Id = 3, Name = "Mystery" },
            new Genre { Id = 4, Name = "Thriller" },
            new Genre { Id = 5, Name = "Romance" },
            new Genre { Id = 6, Name = "Historical Fiction" },
            new Genre { Id = 7, Name = "Horror" },
            new Genre { Id = 8, Name = "Biography" },
            new Genre { Id = 9, Name = "Self-Help" },
            new Genre { Id = 10, Name = "Poetry" },
            new Genre { Id = 11, Name = "Young Adult" },
            new Genre { Id = 12, Name = "Children's" },
            new Genre { Id = 13, Name = "Adventure" },
            new Genre { Id = 14, Name = "Action" },
            new Genre { Id = 15, Name = "Literary Fiction" }
        ];
    }

    private static List<Book> PrepareBookModels()
    // todo: add more when the models are complete
    {
        const int tolkienAuthorId = 1; // todo: change this when some Authors are seeded
        return
        [
            new Book
            {
                Id = 1,
                Title = "The Fellowship of the Ring",
                ISBN = "978-0618260243",
                Price = 12.99M,
                Description =
                    "The first volume in J.R.R. Tolkien's epic adventure, starting the journey to destroy the One Ring.",
                AuthorId = tolkienAuthorId
            },

            new Book
            {
                Id = 2,
                Title = "The Two Towers",
                ISBN = "978-0618260281",
                Price = 14.50M,
                Description =
                    "The second volume of the trilogy, where the fellowship is scattered and the war for Middle-earth escalates.",
                AuthorId = tolkienAuthorId
            },

            new Book
            {
                Id = 3,
                Title = "The Return of the King",
                ISBN = "978-0618260304",
                Price = 15.99M,
                Description =
                    "The final volume, chronicling the final destruction of the Ring and the ultimate fate of Middle-earth.",
                AuthorId = tolkienAuthorId
            }
        ];
    }

    private static List<Rating> PrepareRatingModels()
    {
        var seedDate = new DateTime(2025, 09, 15); // todo: randomize this a bit

        return
        [
            new Rating
            {
                Id = 1,
                Stars = 5,
                BookId = 1,
                DateCreated = seedDate,
                DateModified = seedDate
            },

            new Rating
            {
                Id = 2,
                Stars = 4,
                BookId = 2,
                DateCreated = seedDate,
                DateModified = seedDate
            }
        ]; 
    }
    
}