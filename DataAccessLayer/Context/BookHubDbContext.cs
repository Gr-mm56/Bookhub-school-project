using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class BookHubDbContext: IdentityDbContext<LocalIdentityUser>
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public new DbSet<User> Users { get; set; }
    public DbSet<LocalIdentityUser> LocalIdentityUsers { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<PurchaseItem> PurchaseItems { get; set; }
    public DbSet<WishlistItem> WishlistItems { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Publisher> Publishers { get; set; }
    public DbSet<Image> Images { get; set; }
    public DbSet<AuditLog> AuditLogs { get; set; }

    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Let IdentityDbContext configure identity-related entities first
        base.OnModelCreating(modelBuilder);

        // Book M:N Genre
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Genres)
            .WithMany(g => g.Books)
            .UsingEntity<RelBookGenre>(
                j => j.HasOne(x => x.Genre).WithMany().OnDelete(DeleteBehavior.Restrict),
                j => j.HasOne(x => x.Book).WithMany().OnDelete(DeleteBehavior.Cascade)
            );

        // Book M:N Author
        modelBuilder.Entity<Book>()
            .HasMany(b => b.Authors)
            .WithMany(a => a.Books)
            .UsingEntity<RelBookAuthor>(
                j => j.HasOne(x => x.Author).WithMany().OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne(x => x.Book).WithMany().OnDelete(DeleteBehavior.Cascade)
            );

        // One-to-many relationships
        modelBuilder.Entity<User>().HasMany(u => u.Carts);
        modelBuilder.Entity<User>().HasMany(u => u.WishlistItems);
        modelBuilder.Entity<User>().HasMany(u => u.Ratings);
        modelBuilder.Entity<Cart>().HasMany(u => u.PurchaseItems);
        modelBuilder.Entity<Publisher>().HasMany(b => b.Books);

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

        // Book 1 Image
        modelBuilder.Entity<Book>()
            .HasOne(b => b.Image)
            .WithMany();

        // Publisher 0..1 Image
        modelBuilder.Entity<Publisher>()
            .HasOne(p => p.ProfilePhoto)
            .WithMany();

        // Book -> Rating - On Delete Cascade
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.Book)
            .WithMany(b => b.Ratings)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Cart - On Delete Cascade
        modelBuilder.Entity<Cart>()
            .HasOne(c => c.User)
            .WithMany(u => u.Carts)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> WishlistItem - On Delete Cascade
        modelBuilder.Entity<WishlistItem>()
            .HasOne(w => w.User)
            .WithMany(u => u.WishlistItems)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Rating - On Delete Cascade
        modelBuilder.Entity<Rating>()
            .HasOne(r => r.User)
            .WithMany(u => u.Ratings)
            .OnDelete(DeleteBehavior.Cascade);

        // User -> Image - On Delete Cascade
        modelBuilder.Entity<User>()
            .HasOne(u => u.ProfilePhoto)
            .WithOne(i => i.User)
            .OnDelete(DeleteBehavior.Cascade);

        // Author -> Image - On Delete Cascade
        modelBuilder.Entity<Author>()
            .HasOne(a => a.ProfilePhoto)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // Cart -> PurchaseItem - On Delete Cascade
        modelBuilder.Entity<PurchaseItem>()
            .HasOne(p => p.Cart)
            .WithMany(c => c.PurchaseItems)
            .OnDelete(DeleteBehavior.Cascade);

        // Book -> PurchaseItem - On Delete Cascade
        modelBuilder.Entity<PurchaseItem>()
            .HasOne(p => p.Book)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        // Book -> PurchaseItem - On Delete Cascade
        modelBuilder.Entity<WishlistItem>()
            .HasOne(p => p.Book)
            .WithMany()
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Seed();
    }
}
