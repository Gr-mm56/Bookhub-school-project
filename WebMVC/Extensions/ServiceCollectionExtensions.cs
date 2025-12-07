using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using Infrastructure.Repository;
using Microsoft.Extensions.FileProviders;

namespace WebMVC.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBusinessServices(
        this IServiceCollection services)
    {
        services.AddScoped<IRatingService, RatingService>();
        services.AddScoped<IGenreService, GenreService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<ICartService, CartService>();
        services.AddScoped<IPurchaseItemService, PurchaseItemService>();
        services.AddScoped<IWishlistItemService, WishlistItemService>();
        services.AddScoped<IBookService, BookService>();
        services.AddScoped<IAuthorService, AuthorService>();
        services.AddScoped<IPublisherService, PublisherService>();
        services.AddScoped<IImageService, ImageService>();
        
        services.AddScoped<ISearchFacade, SearchFacade>();
        services.AddScoped<IBookManagementFacade, BookManagementFacade>();

        return services;
    }

    public static IServiceCollection AddUploadService(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var (storagePath, virtualBase) = ResolveStoragePath(configuration, environment);

        services.AddSingleton<IUploadRepository>(new UploadRepository(storagePath, virtualBase));
        
        services.AddSingleton<IUploadService, FileSystemUploadService>();

        return services;
    }

    public static void ConfigureStaticFileServing(
        this WebApplication app,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var (storagePath, virtualBase) = ResolveStoragePath(configuration, environment);

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(storagePath),
            RequestPath = virtualBase
        });
    }

    private static (string storagePath, string virtualBase) ResolveStoragePath(
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var storageRel = configuration["ImageStorage:Path"] ?? "assets/uploads";
        var relPathNormalized = storageRel
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar)
            .Trim(Path.DirectorySeparatorChar);

        var current = new DirectoryInfo(environment.ContentRootPath);
        var solutionRoot = current;
        while (solutionRoot != null && solutionRoot.GetFiles("*.sln").Length == 0)
        {
            solutionRoot = solutionRoot.Parent;
        }

        var baseFolder = solutionRoot?.FullName ?? environment.ContentRootPath;
        var storagePath = Path.Combine(baseFolder, relPathNormalized);

        if (!Directory.Exists(storagePath))
        {
            Directory.CreateDirectory(storagePath);
        }

        var virtualBase = "/" + relPathNormalized.Replace(Path.DirectorySeparatorChar, '/');

        return (storagePath, virtualBase);
    }
}

