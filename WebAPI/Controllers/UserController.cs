using BusinessLayer.Models.Common;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Services.Interfaces;
using BusinessLayer.Models.User.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class UserController(IUserService userService) : Controller
{
    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllUsers([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
        {
            return ValidationProblem(ModelState);
        }

        var result = await userService.GetUsersAsync(pagedRequest.Limit, pagedRequest.Offset);

        if (result.Items.Any())
        {
            return Ok(result);
        }

        return NoContent();
    }

    [HttpGet]
    [Route("details/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        UserDto? user = await userService.GetUserByIdAsync(id);

        return user == null ? NotFound() : Ok(user);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser([FromBody] UserCreateDto user)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await userService.CreateUserAsync(user);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> UpdateGenre(int id, [FromBody] UserUpdateDto requestDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await userService.UpdateUserAsync(id, requestDto);
        if (user == null)
        {
            return NotFound();
        }

        return Ok(user);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> DeleteUser(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        bool result = await userService.DeleteUserAsync(id);

        if (!result)
        {
            return NotFound();
        }

        return NoContent();
    }
}
