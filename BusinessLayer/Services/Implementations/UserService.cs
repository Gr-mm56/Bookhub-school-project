using BusinessLayer.Mappers;
using BusinessLayer.Models.Common;
using BusinessLayer.Models.User.Requests;
using BusinessLayer.Models.User.Responses;
using BusinessLayer.Services.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLayer.Services.Implementations;

public class UserService : BaseService<BookHubDbContext>, IUserService
{
    public UserService(BookHubDbContext context): base(context)
    {
    }

    public async Task<PagedResultDto<UserDto>> GetAllAsync(int limit = 20, int offset = 0)
    {
        var query = Context.Users
            .AsNoTracking()
            .OrderBy(u => u.Id);

        return await PageAsync(query, limit, offset, UserMapper.ToDtoList);
    }

    public async Task<UserDto?> GetByIdAsync(int id)
    {
        var user = await Context.Users
            .AsNoTracking()
            .FirstOrDefaultAsync(u => u.Id == id);

        return user != null ? UserMapper.ToDto(user) : null;

    }

    public async Task<UserDto> CreateAsync(UserCreateDto userCreateDto)
    {
        User user = UserMapper.CreateDtoToEntity(userCreateDto);

        await Context.Users.AddAsync(user);
        await SaveAsync();

        return UserMapper.ToDto(user);

    }

    public async Task<bool> DeleteAsync(int id)
    {
        User? user = await Context.Users.FirstOrDefaultAsync(g => g.Id == id);
        if (user == null)
            return false;

        Context.Users.Remove(user);
        await SaveAsync();
        return true;
    }

    public async Task<UserDto?> UpdateAsync(int id, UserUpdateDto userUpdateDto)
    {
        User? user = await Context.Users.FirstOrDefaultAsync(u => u.Id == id);
        if (user == null)
            return null;

        UserMapper.UpdateEntity(user, userUpdateDto);
        await SaveAsync();

        return UserMapper.ToDto(user);
    }
}
