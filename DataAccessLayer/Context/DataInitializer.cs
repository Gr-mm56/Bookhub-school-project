using System.Collections;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public static class DataInitializer
{
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

        var bookAuthors = PrepareBookAuthorRelationships();
        modelBuilder.Entity<Rel_Book_Author>().HasData(bookAuthors);


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
    {
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
                ImageId = 6
            },

            new Book
            {
                Id = 2,
                Title = "The Two Towers",
                ISBN = "978-0618260281",
                Price = 14.50,
                Description =
                    "The second volume of the trilogy, where the fellowship is scattered and the war for Middle-earth escalates.",
                ImageId = 7
            },

            new Book
            {
                Id = 3,
                Title = "The Return of the King",
                ISBN = "978-0618260304",
                Price = 15.99,
                Description =
                    "The final volume, chronicling the final destruction of the Ring and the ultimate fate of Middle-earth.",
                ImageId = 8
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
                ProfilePhotoId = 1
            },
            new User
            {
                Id = 2,
                Name = "Jane",
                Surname = "Smith",
                Country = "UK",
                City = "London",
                Street = "Baker Street 221B",
                ProfilePhotoId = 2
            },
            new User
            {
                Id = 3,
                Name = "Taro",
                Surname = "Yamada",
                Country = "Japan",
                City = "Tokyo",
                Street = "Shibuya 1-2-3",
                ProfilePhotoId = 3
            },
            new User
            {
                Id = 4,
                Name = "Anna",
                Surname = "Kowalska",
                Country = "Poland",
                City = "Warsaw",
                Street = "Marszałkowska 45",
                ProfilePhotoId = 4
            },
            new User
            {
                Id = 5,
                Name = "Peter",
                Surname = "Novák",
                Country = "Slovakia",
                City = "Bratislava",
                Street = "Hviezdoslavovo námestie 7",
                ProfilePhotoId = 5
            }
        ];
    }

    private static List<Rating> PrepareRatingModels()
    {
        var seedDate = new DateTime(2025, 09, 15);

        return
        [
            new Rating
            {
                Id = 1,
                Stars = 5,
                BookId = 1,
                UserId = 1
            },

            new Rating
            {
                Id = 2,
                Stars = 4,
                BookId = 2,
                UserId = 2
            }
        ];
    }

    private static List<Cart> PrepareCartModels()
    {
        var seedDate = new DateTime(2025, 09, 15);

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
                OrderDate = seedDate
            },
            new Cart
            {
                Id = 4,
                UserId = 4,
                TotalValue = 15.75,
                OrderId = 1002,
                OrderDate = seedDate
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
        return
        [
            new Author
            {
                Id = 1,
                Name = "J.R.R.",
                Surname = "Tolkien",
                ProfilePhotoId = 9
            },
            new Author
            {
                Id = 2,
                Name = "J.K.",
                Surname = "Rowling",
                ProfilePhotoId = 10
            }
        ];
    }

    private static List<Publisher> PreparePublisherModels()
    {
        return
        [
            new Publisher
            {
                Id = 1,
                Name = "HarperCollins",
                Address = "195 Broadway, New York, NY 10007, USA",
                ProfilePhotoId = 11
            },
            new Publisher
            {
                Id = 2,
                Name = "Penguin Random House",
                Address = "1745 Broadway, New York, NY 10019, USA",
                ProfilePhotoId = 12
            }
        ];
    }

    private static List<Rel_Book_Author> PrepareBookAuthorRelationships()
    {
        return
        [
            // Tolkien's books
            new Rel_Book_Author { BookId = 1, AuthorId = 1 },
            new Rel_Book_Author { BookId = 2, AuthorId = 1 },
            new Rel_Book_Author { BookId = 3, AuthorId = 1 },
        
            // Add fictional collaborations to demonstrate many-to-many
            // Book 1 has both Tolkien and Rowling as co-authors (fictional example)
            new Rel_Book_Author { BookId = 1, AuthorId = 2 },
        
            // Book 3 also has a secondary author (fictional example)
            new Rel_Book_Author { BookId = 3, AuthorId = 2 }
        ];
    }
}
