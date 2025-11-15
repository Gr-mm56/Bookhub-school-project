using BusinessLayer.Services.Implementations;
using BusinessLayer.Services.Interfaces;
using Microsoft.Extensions.FileProviders;

namespace WebAPI.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFileSystemUploadService(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        services.AddSingleton<IUploadService>(_ =>
        {
            var (storagePath, virtualBase) = ResolveStoragePath(configuration, environment);
            return new FileSystemUploadService(storagePath, virtualBase);
        });

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
    // Helper method to resolve storage path and virtual base URL
    // files should be stored outside the web root 
    private static (string storagePath, string virtualBase) ResolveStoragePath(
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        var storageRel = configuration["ImageStorage:Path"] ?? "assets/uploads";
        var relPathNormalized = storageRel
            .Replace('/', Path.DirectorySeparatorChar)
            .Replace('\\', Path.DirectorySeparatorChar)
            .Trim(Path.DirectorySeparatorChar);

        // Find solution root
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