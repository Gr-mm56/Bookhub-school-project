namespace Infrastructure.Repository;

public class UploadRepository : IUploadRepository
{
    private readonly string _storagePath; // absolute path on disk
    private readonly string _virtualBasePath; // eg "/assets/uploads"

    /// <summary>
    /// Constructor expects an absolute storage path and the public/virtual base path.
    /// </summary>
    public UploadRepository(string storagePath, string virtualBasePath)
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

        // Return relative path without leading slash (e.g., "assets/uploads/filename.jpg")
        var virtualBase = _virtualBasePath.TrimStart('/');
        var virtualPath = virtualBase + "/" + fileName;
        return virtualPath;
    }

    public async Task<bool> DeleteImageAsync(string virtualPath)
    {
        if (string.IsNullOrWhiteSpace(virtualPath))
        {
            return false;
        }

        try
        {
            // Extract filename from virtual path (e.g., "/assets/uploads/filename.jpg" -> "filename.jpg")
            var fileName = Path.GetFileName(virtualPath);
            var filePath = Path.Combine(_storagePath, fileName);

            if (!File.Exists(filePath))
            {
                return false;
            }

            File.Delete(filePath);
            return true;
        }
        catch
        {
            return false;
        }
    }
}