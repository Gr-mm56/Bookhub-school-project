using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Bogus;

namespace DataAccessLayer.Context;

public static class DataInitializer
{
    private const int BogusSeed = 696969;
    public static void Seed(this ModelBuilder modelBuilder)
    {
        var images = PrepareImageModels();
        modelBuilder.Entity<Image>().HasData(AddDates(images));

        var authors = PrepareAuthorModels();
        modelBuilder.Entity<Author>().HasData(AddDates(authors));

        var publishers = PreparePublisherModels();
        modelBuilder.Entity<Publisher>().HasData(AddDates(publishers));

        var genres = PrepareGenreModels();
        modelBuilder.Entity<Genre>().HasData(AddDates(genres));

        var books = PrepareBookModels();
        modelBuilder.Entity<Book>().HasData(AddDates(books));

        var users = PrepareUserModels();
        modelBuilder.Entity<User>().HasData(AddDates(users));

        var ratings = PrepareRatingModels();
        modelBuilder.Entity<Rating>().HasData(AddDates(ratings));

        var carts = PrepareCartModels();
        modelBuilder.Entity<Cart>().HasData(AddDates(carts));

        var purchaseItems = PreparePurchaseItemModels();
        modelBuilder.Entity<PurchaseItem>().HasData(AddDates(purchaseItems));

        var wishlistItems = PrepareWishlistItemModels();
        modelBuilder.Entity<WishlistItem>().HasData(AddDates(wishlistItems));

        var bookAuthors = PrepareBookAuthorRelationships(books, authors);
        modelBuilder.Entity<RelBookAuthor>().HasData(bookAuthors);

        var bookGenres = PrepareBookGenreRelationships();
        modelBuilder.Entity<RelBookGenre>().HasData(bookGenres);


    }
    private static readonly List<string> GenreNames =
    [
        "Science Fiction", "Fantasy", "Mystery", "Thriller", "Romance",
        "Historical Fiction", "Horror", "Biography", "Self-Help", "Poetry",
        "Young Adult", "Children's", "Adventure", "Action", "Literary Fiction"
    ];
    private static List<Genre> PrepareGenreModels()
    {
        var randomizer = new Randomizer(BogusSeed);
        var shuffledNames = randomizer.Shuffle(GenreNames).ToList();
        return shuffledNames.Select((name, index) => new Genre
        {
            Id = index + 1,
            Name = name
        }).ToList();
    }

    private static List<Book> PrepareBookModels()
    {
        var index = 1;
        return new Faker<Book>().UseSeed(BogusSeed).RuleFor(b => b.Id, _ => index++)
            .RuleFor(b => b.Title, f => f.Lorem.Sentence(2, 5))
            .RuleFor(b => b.ISBN, f => f.Random.ReplaceNumbers("666-1-#######-##-#"))
            .RuleFor(b => b.Price, f => Math.Round(f.Random.Double(5, 50), 2))
            .RuleFor(b => b.Description, f => f.Lorem.Paragraphs(1, 2))
            .RuleFor(b => b.ImageId, f => f.Random.Number(1, 4))
            .RuleFor(b => b.PublisherId, f => f.Random.Number(1, 2))
            .Generate(25);
    }



    private static List<User> PrepareUserModels()
    {
        var index = 1;
        return new Faker<User>().UseSeed(BogusSeed).RuleFor(u => u.Id, _ => index++)
            .RuleFor(u => u.Name, f => f.Name.FirstName())
            .RuleFor(u => u.Surname, f => f.Name.LastName())
            .RuleFor(u => u.Country, f => f.Address.Country())
            .RuleFor(u => u.City, f => f.Address.City())
            .RuleFor(u => u.Street, f => f.Address.StreetName())
            .Generate(7);
    }

    private static List<Rating> PrepareRatingModels()
    {
        const int maxBooks = 25;
        const int maxUsers = 7;
        var targetCount = Math.Min(8, maxBooks * maxUsers);

        var allPairs = (from bookId in Enumerable.Range(1, maxBooks)
            from userId in Enumerable.Range(1, maxUsers)
            select (BookId: bookId, UserId: userId)).ToList();

        var randomizer = new Randomizer(BogusSeed);
        var shuffledPairs = randomizer.Shuffle(allPairs).Take(targetCount).ToList();

        var id = 1;
        var pairIndex = 0;

        return new Faker<Rating>().UseSeed(BogusSeed)
            .RuleFor(r => r.Id, _ => id++)
            .RuleFor(r => r.BookId, _ => shuffledPairs[pairIndex].BookId)
            .RuleFor(r => r.UserId, _ => {
                var u = shuffledPairs[pairIndex].UserId;
                pairIndex++;
                return u;
            })
            .RuleFor(r => r.Stars, f => f.Random.Number(1, 5))
            .Generate(targetCount);
    }

    private static List<Cart> PrepareCartModels()
    {
        const int userCount = 7;
        var idCounter = 1;
        var userCounter = 1;

        return new Faker<Cart>().UseSeed(BogusSeed)
            .RuleFor(c => c.Id, _ => idCounter++)
            .RuleFor(c => c.UserId, _ => userCounter++)
            .RuleFor(c => c.TotalValue, f => Math.Round(f.Random.Double(0, 300), 2))
            .RuleFor(c => c.PaymentStatus, f => f.Random.Bool(0.5f) ? 1 : 0)
            .RuleFor(c => c.OrderId, f => f.Random.Bool(0.5f) ? f.Random.Int(1000, 2000) : null)
            .RuleFor(c => c.OrderDate, (f, c) => c.OrderId.HasValue ? f.Date.Between(new DateTime(2024, 1, 1), new DateTime(2025, 9, 15)) : null)
             .Generate(userCount);
    }

    private static List<PurchaseItem> PreparePurchaseItemModels()
    {
        const int maxBooks = 25;
        const int maxCarts = 7;

        var allPairs = (from b in Enumerable.Range(1, maxBooks)
            from c in Enumerable.Range(1, maxCarts)
            select (BookId: b, CartId: c)).ToList();

        var randomizer = new Randomizer(BogusSeed);
        var targetCount = Math.Min(10, allPairs.Count);
        var shuffled = randomizer.Shuffle(allPairs).Take(targetCount).ToList();

        var id = 1;
        var pairIndex = 0;

        return new Faker<PurchaseItem>().UseSeed(BogusSeed)
            .RuleFor(p => p.Id, _ => id++)
            .RuleFor(p => p.BookId, _ => shuffled[pairIndex].BookId)
            .RuleFor(p => p.CartId, _ => { var v = shuffled[pairIndex].CartId; pairIndex++; return v; })
            .RuleFor(p => p.Count, f => f.Random.Int(1, 5))
            .Generate(targetCount);
    }

    private static List<WishlistItem> PrepareWishlistItemModels()
    {
        const int maxUsers = 7;
        const int maxBooks = 25;
        var allPairs = (from u in Enumerable.Range(1, maxUsers)
            from b in Enumerable.Range(1, maxBooks)
            select (UserId: u, BookId: b)).ToList();

        var randomizer = new Randomizer(BogusSeed);
        var targetCount = Math.Min(5, allPairs.Count);
        var shuffled = randomizer.Shuffle(allPairs).Take(targetCount).ToList();

        var id = 1;
        var pairIndex = 0;

        return new Faker<WishlistItem>().UseSeed(BogusSeed)
            .RuleFor(w => w.Id, _ => id++)
            .RuleFor(w => w.UserId, _ => shuffled[pairIndex].UserId)
            .RuleFor(w => w.BookId, _ => { var v = shuffled[pairIndex].BookId; pairIndex++; return v; })
            .Generate(targetCount);
    }

    private static List<T> AddDates<T>(List<T> data) where T : BaseEntity
    {
        var creationDate = new DateTime(2025, 09, 16);
        var updateDate = new DateTime(2025, 09, 17);

        foreach (var entity in data)
        {
            entity.CreatedAt = creationDate;
            entity.UpdatedAt = updateDate;
        }

        return data;
    }
    // the urls should not be seeded by Bogus, so the images actually show
    private static List<Image> PrepareImageModels()
    {
        return
        [
            new Image { Id = 1, FileUrl = "assets/users/john_doe.jpg" },
            new Image { Id = 2, FileUrl = "assets/users/jane_smith.jpg" },
            new Image { Id = 3, FileUrl = "assets/users/taro_yamada.jpg" },
            new Image { Id = 4, FileUrl = "assets/users/anna_kowalska.jpg" },
            new Image { Id = 5, FileUrl = "assets/users/peter_novak.jpg" },
            new Image { Id = 6, FileUrl = "assets/books/fellowship_of_the_ring.jpg" },
            new Image { Id = 7, FileUrl = "assets/books/two_towers.jpg" },
            new Image { Id = 8, FileUrl = "assets/books/return_of_the_king.jpg" },
            new Image { Id = 9, FileUrl = "assets/authors/tolkien.jpg" },
            new Image { Id = 10, FileUrl = "assets/authors/rowling.jpg" },
            new Image { Id = 11, FileUrl = "assets/publishers/harpercollins.jpg" },
            new Image { Id = 12, FileUrl = "assets/publishers/penguin.jpg" }
        ];
    }

    private static List<Author> PrepareAuthorModels()
    {
        var index = 1;
        return new Faker<Author>().UseSeed(BogusSeed)
            .RuleFor(a => a.Id, _ => index++)
            .RuleFor(a => a.Name, f => f.Name.FirstName())
            .RuleFor(a => a.Surname, f => f.Name.LastName())
            .RuleFor(a => a.ProfilePhotoId, f => f.Random.Number(1, 7))
            .Generate(5);
    }

    private static List<Publisher> PreparePublisherModels()
    {
        var index = 1;
        return new Faker<Publisher>().UseSeed(BogusSeed)
            .RuleFor(p => p.Id, _ => index++)
            .RuleFor(p => p.Name, f => f.Company.CompanyName())
            .RuleFor(p => p.Address, f => f.Address.FullAddress())
            .RuleFor(p => p.ProfilePhotoId, f => f.Random.Number(1, 4))
            .Generate(3);
    }

    private static List<RelBookGenre> PrepareBookGenreRelationships()
    {
        const int bookCount = 25;
        const int genreCount = 15;

        var randomizer = new Randomizer(BogusSeed);
        var result = new List<RelBookGenre>();

        for (var bookId = 1; bookId <= bookCount; bookId++)
        {
            var genresToPick = randomizer.Number(1, 3);

            var allGenreIds = Enumerable.Range(1, genreCount).ToList();
            var shuffledGenreIds = randomizer.Shuffle(allGenreIds).Take(genresToPick);

            result.AddRange(shuffledGenreIds.Select(g => new RelBookGenre { BookId = bookId, GenreId = g }));
        }

        return result;
    }
    private static List<RelBookAuthor> PrepareBookAuthorRelationships(List<Book> books, List<Author> authors)
    {
        var randomizer = new Randomizer(BogusSeed);
        var authorIds = authors.Select(a => a.Id).ToList();
        var result = new List<RelBookAuthor>();

        foreach (var book in books)
        {
            var maxAuthorsForBook = Math.Min(2, authorIds.Count);
            var authorsToPick = randomizer.Number(1, maxAuthorsForBook);

            var picked = randomizer.Shuffle(authorIds).Take(authorsToPick).ToList();

            result.AddRange(picked.Select(aId => new RelBookAuthor { BookId = book.Id, AuthorId = aId }));
        }

        return result;
    }
}
