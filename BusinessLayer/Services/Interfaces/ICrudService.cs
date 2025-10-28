using BusinessLayer.Models.Common;

namespace BusinessLayer.Services.Interfaces;

public interface ICrudService<TEntityDto, TEntityDetailDto, TCreateDto, TUpdateDto>
{
    Task<PagedResultDto<TEntityDto>> GetAllAsync(int limit, int offset);
    Task<TEntityDetailDto?> GetByIdAsync(int id);
    Task<TEntityDto> CreateAsync(TCreateDto dto);
    Task<TEntityDto?> UpdateAsync(int id, TUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
