using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects;

public class MockedDependencyInjectionBuilder
{
    private IServiceCollection _serviceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder AddMockedDbContext()
    {
        _serviceCollection = _serviceCollection
            .AddDbContext<BookHubDbContext>(options => options
                .UseInMemoryDatabase(MockedDbContext.RandomDbName));

        return this;
    }

    // Add a seeded DbContext instance (uses TestDataHelper via MockedDbContext.CreateFromOptions)
    public MockedDependencyInjectionBuilder AddSeededDbContext()
    {
        var options = MockedDbContext.GenerateNewInMemoryDbContextOptions();
        var seededContext = MockedDbContext.CreateFromOptions(options);

        _serviceCollection = _serviceCollection
            .AddScoped<BookHubDbContext>(_ => seededContext);

        return this;
    }

    public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
        where T : class
    {
        _serviceCollection = _serviceCollection
            .AddScoped<T>(_ => objectToRegister);

        return this;
    }

    public MockedDependencyInjectionBuilder AddServices()
    {
        _serviceCollection = _serviceCollection
            .AddMemoryCache()
            .AddScoped<IRatingService, RatingService>()
            .AddScoped<IGenreService, GenreService>()
            .AddScoped<IUserService, UserService>()
            .AddScoped<ICartService, CartService>()
            .AddScoped<IPurchaseItemService, PurchaseItemService>()
            .AddScoped<IWishlistItemService, WishlistItemService>()
            .AddScoped<IBookService, BookService>()
            .AddScoped<IAuthorService, AuthorService>()
            .AddScoped<IPublisherService, PublisherService>()
            .AddScoped<IImageService, ImageService>();

        return this;
    }
    

    public ServiceProvider Create()
    {
        return _serviceCollection.BuildServiceProvider();
    }
}