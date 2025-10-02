using DataAccessLayer.Entities;
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

        var users = PrepareUserModels();
        modelBuilder.Entity<User>().HasData(users);

        var ratings = PrepareRatingModels();
        modelBuilder.Entity<Rating>().HasData(ratings);

        var carts = PrepareCartModels();
        modelBuilder.Entity<Cart>().HasData(carts);

        var purchaseItems = PreparePurchaseItemModels();
        modelBuilder.Entity<PurchaseItem>().HasData(purchaseItems);

        var wishlistItems = PrepareWishlistItemModels();
        modelBuilder.Entity<WishlistItem>().HasData(wishlistItems);
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
                Price = 12.99,
                Description =
                    "The first volume in J.R.R. Tolkien's epic adventure, starting the journey to destroy the One Ring.",
                AuthorId = tolkienAuthorId
            },

            new Book
            {
                Id = 2,
                Title = "The Two Towers",
                ISBN = "978-0618260281",
                Price = 14.50,
                Description =
                    "The second volume of the trilogy, where the fellowship is scattered and the war for Middle-earth escalates.",
                AuthorId = tolkienAuthorId
            },

            new Book
            {
                Id = 3,
                Title = "The Return of the King",
                ISBN = "978-0618260304",
                Price = 15.99,
                Description =
                    "The final volume, chronicling the final destruction of the Ring and the ultimate fate of Middle-earth.",
                AuthorId = tolkienAuthorId
            }
        ];
    }

    private static List<User> PrepareUserModels()
    {
        // TODO seed images
        return
        [
            new User
            {
                Id = 1,
                Name = "John",
                Surname = "Doe",
                Country = "USA",
                City = "New York",
                Street = "5th Avenue 123",
                ImageId = 1
            },
            new User
            {
                Id = 2,
                Name = "Jane",
                Surname = "Smith",
                Country = "UK",
                City = "London",
                Street = "Baker Street 221B",
                ImageId = 2
            },
            new User
            {
                Id = 3,
                Name = "Taro",
                Surname = "Yamada",
                Country = "Japan",
                City = "Tokyo",
                Street = "Shibuya 1-2-3",
                ImageId = 3
            },
            new User
            {
                Id = 4,
                Name = "Anna",
                Surname = "Kowalska",
                Country = "Poland",
                City = "Warsaw",
                Street = "Marszałkowska 45",
                ImageId = 4
            },
            new User
            {
                Id = 5,
                Name = "Peter",
                Surname = "Novák",
                Country = "Slovakia",
                City = "Bratislava",
                Street = "Hviezdoslavovo námestie 7",
                ImageId = 5
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

    private static List<Cart> PrepareCartModels()
    {
        return
        [
            new Cart
            {
                Id = 1,
                UserId = 1,
                TotalValue = 49.99,
                OrderId = null,
                OrderDate = null
            },
            new Cart
            {
                Id = 2,
                UserId = 2,
                TotalValue = 0,
                OrderId = null,
                OrderDate = null
            },
            new Cart
            {
                Id = 3,
                UserId = 3,
                TotalValue = 120.50,
                OrderId = 1001,
                OrderDate = DateTime.UtcNow.AddDays(-7)
            },
            new Cart
            {
                Id = 4,
                UserId = 4,
                TotalValue = 15.75,
                OrderId = 1002,
                OrderDate = DateTime.UtcNow.AddDays(-1)
            },
            new Cart
            {
                Id = 5,
                UserId = 5,
                TotalValue = 200.00,
                OrderId = null,
                OrderDate = null
            }
        ];
    }

    private static List<PurchaseItem> PreparePurchaseItemModels()
    {
        return
        [
            new PurchaseItem
            {
                Id = 1,
                BookId = 1,
                CartId = 1,
                Count = 2
            },
            new PurchaseItem
            {
                Id = 2,
                BookId = 3,
                CartId = 1,
                Count = 1
            },
            new PurchaseItem
            {
                Id = 3,
                BookId = 2,
                CartId = 3,
                Count = 1
            },
            new PurchaseItem
            {
                Id = 4,
                BookId = 5,
                CartId = 4,
                Count = 3
            },
            new PurchaseItem
            {
                Id = 5,
                BookId = 4,
                CartId = 5,
                Count = 1
            }
        ];
    }

    private static List<WishlistItem> PrepareWishlistItemModels()
    {
        return
        [
            new WishlistItem
            {
                Id = 1,
                UserId = 1,
                BookId = 2
            },
            new WishlistItem
            {
                Id = 2,
                UserId = 1,
                BookId = 5
            },
            new WishlistItem
            {
                Id = 3,
                UserId = 2,
                BookId = 1
            },
            new WishlistItem
            {
                Id = 4,
                UserId = 3,
                BookId = 3
            },
            new WishlistItem
            {
                Id = 5,
                UserId = 4,
                BookId = 4
            }
        ];
    }
}
