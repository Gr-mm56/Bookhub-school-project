using BusinessLayer.Models.Common;
using BusinessLayer.Models.Image.Requests;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class ImageController(IImageService imageService) : ControllerBase
{

    [HttpGet]
    [Route("list")]
    public async Task<IActionResult> GetImages([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await imageService.GetImagesAsync(pagedRequest.Limit, pagedRequest.Offset);
        return Ok(result);
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<IActionResult> GetImageById(int id)
    {
        var image = await imageService.GetImageByIdAsync(id);
        if (image == null)
        {
            return NotFound();
        }

        return Ok(image);
    }

    [HttpPost]
    [Route("")]
    public async Task<IActionResult> CreateImage([FromBody] ImageRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var image = await imageService.CreateImageAsync(requestDto);
            return CreatedAtAction(nameof(GetImageById), new { id = image.FileUrl }, image);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<IActionResult> UpdateImage(int id, [FromBody] ImageRequestDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var image = await imageService.UpdateImageAsync(id, requestDto);
            if (image == null)
            {
                return NotFound();
            }

            return Ok(image);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<IActionResult> DeleteImage(int id)
    {
        var result = await imageService.DeleteImageAsync(id);
        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
