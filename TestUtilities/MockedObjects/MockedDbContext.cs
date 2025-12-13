using DataAccessLayer.Context;
using DataAccessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects;

public class MockedDbContext
{
    public static string RandomDbName => Guid.NewGuid().ToString();

    public static DbContextOptions<BookHubDbContext> GenerateNewInMemoryDbContextOptions()
    {
        var dbContextOptions = new DbContextOptionsBuilder<BookHubDbContext>()
            .UseInMemoryDatabase(RandomDbName)
            .Options;

        return dbContextOptions;
    }

    public static BookHubDbContext CreateFromOptions(DbContextOptions<BookHubDbContext> options)
    {
        var httpContextAccessor = Substitute.For<IHttpContextAccessor>();
        var auditLogService = Substitute.For<IAuditLogService>();        
        var dbContextToMock = new BookHubDbContext(options, httpContextAccessor, auditLogService);
        PrepareData(dbContextToMock);

        return dbContextToMock;
    }

    private static void PrepareData(BookHubDbContext dbContext)
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