using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface ICrudService<TEntityDto, TCreateDto, TUpdateDto>
{
    Task<PagedResultDto<TEntityDto>> GetAllAsync(int limit, int offset);
    Task<TEntityDto?> GetByIdAsync(int id);
    Task<TEntityDto> CreateAsync(TCreateDto dto);
    Task<TEntityDto?> UpdateAsync(int id, TUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
