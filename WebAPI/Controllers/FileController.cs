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
        catch (Exception ex)
        {
            return StatusCode(500, "Internal server error: " + ex.Message);
        }
    }
}