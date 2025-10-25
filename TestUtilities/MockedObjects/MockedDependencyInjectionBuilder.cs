using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestUtilities.MockedObjects;

public class MockedDependencyInjectionBuilder
{
    private static string RandomDbName => Guid.NewGuid().ToString();
    private IServiceCollection _serviceCollection = new ServiceCollection();

    public MockedDependencyInjectionBuilder()
    {
    }

    public MockedDependencyInjectionBuilder AddMockedDBContext()
    {
        _serviceCollection = _serviceCollection
            .AddDbContext<BookHubDbContext>(options => options
                .UseInMemoryDatabase(RandomDbName));

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