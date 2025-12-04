namespace Infrastructure.Repository;

public interface IUploadRepository
{
    /// <summary>
    /// Saves a stream to MinIO and returns the object URL.
    /// </summary>
    /// <param name="content">Stream containing file bytes.</param>
    /// <param name="originalFileName">Original file name (used to preserve extension).</param>
    /// <param name="contentType">Optional content type (e.g., "image/jpeg").</param>
    /// <returns>Public URL to access the uploaded file.</returns>
    Task<string> SaveImageAsync(Stream content, string originalFileName, string? contentType = null);

    /// <summary>
    /// Deletes an image file from MinIO.
    /// </summary>
    /// <param name="objectName">Object name/path (e.g., "filename.jpg").</param>
    /// <returns>True if the file was deleted; false if the file was not found.</returns>
    Task<bool> DeleteImageAsync(string objectName);
}