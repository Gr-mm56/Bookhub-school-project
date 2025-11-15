using BusinessLayer.Services.Interfaces;

namespace BusinessLayer.Services.Implementations;

public class FileSystemUploadService : IUploadService
{
    private readonly string _storagePath; // absolute path on disk
    private readonly string _virtualBasePath; // eg "/assets/uploads"

    /// <summary>
    /// Constructor expects an absolute storage path and the public/virtual base path.
    /// </summary>
    public FileSystemUploadService(string storagePath, string virtualBasePath)
    {
        _storagePath = storagePath ?? throw new ArgumentNullException(nameof(storagePath));
        _virtualBasePath = (virtualBasePath).TrimEnd('/');

        Directory.CreateDirectory(_storagePath);
    }

    public async Task<string> SaveImageAsync(Stream content, string originalFileName, string? contentType = null)
    {
        if (content is not { CanRead: true })
        {
            throw new ArgumentException("Content stream is null or unreadable.", nameof(content));
        }

        var ext = Path.GetExtension(originalFileName);
        var fileName = $"{Guid.NewGuid()}{ext}";
        var filePath = Path.Combine(_storagePath, fileName);

        // write to disk
        await using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
        {
            await content.CopyToAsync(fs);
            await fs.FlushAsync();
        }

        var virtualPath = _virtualBasePath + "/" + fileName;
        return virtualPath;
    }
}

