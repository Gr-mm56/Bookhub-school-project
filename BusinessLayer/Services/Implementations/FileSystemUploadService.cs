using BusinessLayer.Services.Interfaces;
using Infrastructure.Repository;

namespace BusinessLayer.Services.Implementations;

public class FileSystemUploadService : IUploadService
{
    private readonly IUploadRepository _uploadRepository;
    
    public FileSystemUploadService(IUploadRepository uploadRepository)
    {
        _uploadRepository = uploadRepository ?? throw new ArgumentNullException(nameof(uploadRepository));
    }

    public async Task<string> SaveImageAsync(Stream content, string originalFileName, string? contentType = null)
    {
        if (content is not { CanRead: true })
        {
            throw new ArgumentException("Content stream is null or unreadable.", nameof(content));
        }

        if (string.IsNullOrWhiteSpace(originalFileName))
        {
            throw new ArgumentException("File name cannot be empty.", nameof(originalFileName));
        }

        return await _uploadRepository.SaveImageAsync(content, originalFileName, contentType);
    }

    public async Task<bool> DeleteImageAsync(string virtualPath)
    {
        return await _uploadRepository.DeleteImageAsync(virtualPath);
    }
}

