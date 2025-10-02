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

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book M:N Genre
        modelBuilder.Entity<Book>().HasMany(b => b.Genres).WithMany(g => g.Books);

        // One-to-many relationships
        modelBuilder.Entity<User>().HasMany(u => u.Carts);
        modelBuilder.Entity<User>().HasMany(u => u.WishlistItems);
        modelBuilder.Entity<User>().HasMany(u => u.Ratings);
        modelBuilder.Entity<Cart>().HasMany(u => u.PurchaseItems);

        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}
