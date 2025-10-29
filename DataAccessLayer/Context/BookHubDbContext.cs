using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class BookHubDbContext: DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Image> Images { get; set; }

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book M:N Genre
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<RelBookGenre>(
                j => j
                    .HasOne<Genre>()
                    .WithMany()
                    .HasForeignKey(rel => rel.GenreId)
                    .OnDelete(DeleteBehavior.Restrict),
                j => j
                    .HasOne<Book>()
                    .WithMany()
                    .HasForeignKey(rel => rel.BookId)
                    .OnDelete(DeleteBehavior.Cascade)
            );

        // Book M:N Author with cascade delete configuration
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<RelBookAuthor>(
                j => j
                    .HasOne(rel => rel.Author)
                    .WithMany()
                    .HasForeignKey(rel => rel.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(rel => rel.Book)
                    .WithMany()
                    .HasForeignKey(rel => rel.BookId)
                    .OnDelete(DeleteBehavior.Cascade)
            );

        // Author M:N Book with cascade delete configuration
        modelBuilder.Entity<Author>()
            .HasMany(a => a.Books)
            .WithMany(b => b.Authors)
            .UsingEntity<RelBookAuthor>(
                j => j
                    .HasOne(rel => rel.Book)
                    .WithMany()
                    .HasForeignKey(rel => rel.BookId)
                    .OnDelete(DeleteBehavior.Cascade),
                j => j
                    .HasOne(rel => rel.Author)
                    .WithMany()
                    .HasForeignKey(rel => rel.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade)
            );

        // One-to-many relationships
        modelBuilder.Entity<User>().HasMany(u => u.Carts);
        modelBuilder.Entity<User>().HasMany(u => u.WishlistItems);
        modelBuilder.Entity<User>().HasMany(u => u.Ratings);
        modelBuilder.Entity<Cart>().HasMany(u => u.PurchaseItems);
        modelBuilder.Entity<Publisher>().HasMany(b => b.Books);

        // Book 1 Image
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Image)
            .WithMany()
            .HasForeignKey(b => b.ImageId);

        // User 0..1 Image
        modelBuilder.Entity<User>()
            .HasOne(u => u.ProfilePhoto)
            .WithMany()
            .HasForeignKey(u => u.ProfilePhotoId)
            .IsRequired(false);

        // Author 0..1 Image
        modelBuilder.Entity<Author>()
            .HasOne(a => a.ProfilePhoto)
            .WithMany()
            .HasForeignKey(a => a.ProfilePhotoId)
            .IsRequired(false);

        // Publisher 0..1 Image
        modelBuilder.Entity<Publisher>()
            .HasOne(p => p.ProfilePhoto)
            .WithMany()
            .HasForeignKey(p => p.ProfilePhotoId)
            .IsRequired(false);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                 .SelectMany(e => e.GetForeignKeys())
                 .Where(fk =>
                     !(fk.PrincipalEntityType.ClrType == typeof(Book) && fk.DeclaringEntityType.ClrType == typeof(RelBookAuthor)) &&
                     !(fk.PrincipalEntityType.ClrType == typeof(Author) && fk.DeclaringEntityType.ClrType == typeof(RelBookAuthor)) &&
                     !(fk.PrincipalEntityType.ClrType == typeof(Book) && fk.DeclaringEntityType.ClrType == typeof(RelBookGenre)) &&
                     !(fk.PrincipalEntityType.ClrType == typeof(Genre) && fk.DeclaringEntityType.ClrType == typeof(RelBookGenre))
                     ))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        // Book -> Rating - On Delete Cascade
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Ratings)
            .HasForeignKey(r => r.BookId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Cart - On Delete Cascade
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> WishlistItem - On Delete Cascade
        modelBuilder.Entity<WishlistItem>()
            .HasOne(w => w.User)
            .WithMany(u => u.WishlistItems)
            .HasForeignKey(w => w.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Rating - On Delete Cascade
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .HasForeignKey(r => r.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Image - On Delete Cascade
        modelBuilder.Entity<User>()
            .HasOne(u => u.ProfilePhoto)
            .WithOne(i => i.User)
            .HasForeignKey<User>(u => u.ProfilePhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Author -> Image - On Delete Cascade
        modelBuilder.Entity<User>()
            .HasOne(a => a.ProfilePhoto)
            .WithOne(i => i.User)
            .HasForeignKey<User>(a => a.ProfilePhotoId)
            .OnDelete(DeleteBehavior.Cascade);

        // Cart -> PurchaseItem - On Delete Cascade
        modelBuilder.Entity<PurchaseItem>()
            .HasOne(p => p.Cart)
            .WithMany(c => c.PurchaseItems)
            .HasForeignKey(p => p.CartId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
