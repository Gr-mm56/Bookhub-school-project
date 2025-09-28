using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context;

public class BookHubDbContext: DbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Rating> Ratings { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public BookHubDbContext(DbContextOptions<BookHubDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // Book M:N Genre
        modelBuilder.Entity<Book>().HasMany(b => b.Genres).WithMany(g => g.Books);        
        
        foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }

        modelBuilder.Seed();
        base.OnModelCreating(modelBuilder);
    }
}