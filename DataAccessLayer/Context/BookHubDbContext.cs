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
        modelBuilder.Entity<Book>().HasMany(b => b.Genres).WithMany(g => g.Books);

        // Book M:N Author
        modelBuilder.Entity<Book>().HasMany(a => a.Authors).WithMany(b => b.Books).UsingEntity<Rel_Book_Author>();

        // One-to-many relationships
        modelBuilder.Entity<User>().HasMany(u => u.Carts);
        modelBuilder.Entity<User>().HasMany(u => u.WishlistItems);
        modelBuilder.Entity<User>().HasMany(u => u.Ratings);
        modelBuilder.Entity<Cart>().HasMany(u => u.PurchaseItems);
        modelBuilder.Entity<Book>().HasMany(p => p.Publishers);

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

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
