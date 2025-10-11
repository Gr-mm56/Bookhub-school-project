using DataAccessLayer.Entities;
using DataAccessLayer.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers;

public abstract class BaseController<TEntity> : ControllerBase
    where TEntity : BaseEntity
{
    protected readonly IRepository<TEntity> _repository;

    public BaseController(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    protected async Task<IActionResult> GetAll()
    {
        var entities = await _repository.GetAllAsync();

        if (entities.Any())
        {
            return Ok(entities);
        }

        return NoContent();
    }

    [HttpGet]
    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    protected async Task<IActionResult> Get(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        TEntity? entity = await _repository.GetByIdAsync(id);

        return entity == null ? NotFound() : Ok(entity);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    protected async Task<IActionResult> Insert([FromBody] TEntity entity)
    {
        await _repository.InsertAsync(entity);
        return Created();
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    protected async Task<IActionResult> Update(int id, [FromBody] TEntity entity)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        TEntity? u = await _repository.GetByIdAsync(id);

        if (u == null)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    protected async Task<IActionResult> Delete(int id)
    {
        if (id <= 0)
        {
            return BadRequest();
        }

        TEntity? entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            return NotFound();
        }

        await _repository.DeleteAsync(id);
        return NoContent();
    }
}
