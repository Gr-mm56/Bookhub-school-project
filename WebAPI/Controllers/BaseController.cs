using BusinessLayer.Models.Common;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController<TEntityDto, TCreateDto, TUpdateDto, TService> : Controller
    where TService : class
{
    protected readonly TService _service;

    protected BaseController(TService service)
    {
        _service = service;
    }

    [HttpGet("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public abstract Task<IActionResult> GetAll([FromQuery] PagedRequestDto pagedRequest);

    [HttpGet("details/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<IActionResult> GetById(int id);

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public abstract Task<IActionResult> Create([FromBody] TCreateDto entity);

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<IActionResult> Update(int id, [FromBody] TUpdateDto entity);

    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public abstract Task<IActionResult> Delete(int id);
}
