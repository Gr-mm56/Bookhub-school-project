using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FileController : ControllerBase
{
    private readonly IUploadService _upload;

    public FileController(IUploadService upload)
    {
        _upload = upload;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile image)
    {
        if (image.Length == 0)
        {
            return BadRequest("No image uploaded.");
        }

        try
        {
            await using var ms = image.OpenReadStream();
            var virtualPath = await _upload.SaveImageAsync(ms, image.FileName, image.ContentType);
            return Ok(new { path = virtualPath });
        }
        catch (ArgumentException aex)
        {
            return BadRequest(aex.Message);
        }
        catch (NotSupportedException nex)
        {
            return StatusCode(StatusCodes.Status415UnsupportedMediaType, nex.Message);
        }
        catch (PathTooLongException)
        {
            return BadRequest("File path is too long.");
        }
        catch (UnauthorizedAccessException)
        {
            return StatusCode(StatusCodes.Status403Forbidden, "Insufficient permissions to save the file.");
        }
        catch (IOException)
        {
            return StatusCode(StatusCodes.Status503ServiceUnavailable,
                "I/O error while saving the file. Please try again later.");
        }
        catch (Exception)
        {
            return StatusCode(StatusCodes.Status500InternalServerError,
                "An unexpected error occurred while processing the upload.");
        }
    }
}