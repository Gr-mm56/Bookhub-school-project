using BusinessLayer.Models.Common;
using BusinessLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

[ApiController]
[Route("[controller]")]
public abstract class BaseController<TEntityDto, TCreateDto, TUpdateDto, TService> : Controller
    where TService : ICrudService<TEntityDto, TCreateDto, TUpdateDto>
{
    protected readonly TService _service;

    protected BaseController(TService service)
    {
        _service = service;
    }

    [HttpGet]
    [Route("list")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public virtual async Task<IActionResult> GetAll([FromQuery] PagedRequestDto pagedRequest)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var result = await _service.GetAllAsync(pagedRequest.Limit, pagedRequest.Offset);
        return result.Items.Any() ? Ok(result) : NoContent();
    }

    [HttpGet]
    [Route("details/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> GetById(int id)
    {
        if (id <= 0)
            return BadRequest();

        var entity = await _service.GetByIdAsync(id);
        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> Create([FromBody] TCreateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        await _service.CreateAsync(dto);

        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public virtual async Task<IActionResult> Update(int id, [FromBody] TUpdateDto dto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var updated = await _service.UpdateAsync(id, dto);
        return updated == null ? NotFound() : Ok(updated);
    }

    [HttpDelete]
    [Route("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public virtual async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
            return BadRequest();

        bool deleted = await _service.DeleteAsync(id);
        return deleted ? NoContent() : NotFound();
    }
}
