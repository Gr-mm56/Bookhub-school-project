using System.IO;

namespace BusinessLayer.Services.Interfaces;

public interface IUploadService
{
    /// <summary>
    /// Saves a stream (image/file) to the configured storage and returns a virtual request path
    /// (eg. "/assets/uploads/filename.jpg").
    /// </summary>
    /// <param name="content">Stream containing file bytes. Caller is responsible for opening the stream.</param>
    /// <param name="originalFileName">Original file name (used to preserve extension).</param>
    /// <param name="contentType">Optional content type.</param>
    /// <returns>Virtual request path that can be used by clients to access the file.</returns>
    Task<string> SaveImageAsync(Stream content, string originalFileName, string? contentType = null);
}

