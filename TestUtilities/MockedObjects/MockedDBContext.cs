using DataAccessLayer.Context;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects;

public class MockedDBContext
{
    public static string RandomDBName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDBContextOptons()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
            .UseInMemoryDatabase(RandomDBName)
            .Options;

        return dbContextOptions;
    } 
    
    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var dbContextToMock = new BookHubDbContext(options);

        var dbContext = new MockedDbContextBuilder<BookHubDbContext>()
            .UseDbContext(dbContextToMock)
            .UseConstructorWithParameters(options)
            .MockedDbContext;

        PrepareData(dbContext);

        return dbContext;
    }
    public static void PrepareData(BookHubDbContext dbContext)
    {
        dbContext.Images.AddRange(TestDataHelper.GetImages());
        dbContext.Authors.AddRange(TestDataHelper.GetAuthors());
        dbContext.Publishers.AddRange(TestDataHelper.GetPublishers());
        dbContext.Genres.AddRange(TestDataHelper.GetGenres());
        dbContext.Books.AddRange(TestDataHelper.GetBooks());
        dbContext.Users.AddRange(TestDataHelper.GetUsers());
        dbContext.Ratings.AddRange(TestDataHelper.GetRatings());
        dbContext.Carts.AddRange(TestDataHelper.GetCarts());
        dbContext.WishlistItems.AddRange(TestDataHelper.GetWishlistItems());
        dbContext.PurchaseItems.AddRange(TestDataHelper.GetPurchaseItems());

        dbContext.SaveChanges();
    }



}