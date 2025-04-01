namespace api.Services;
using Dtos.User;
using Interfaces;
using Mappers;
using Models;

public class UsersService(IUsersRepository usersRepository)
{
    public async Task<Boolean> Create(CreateUserDto createUserDto)
    {
        try
        {
            var existingUser = await usersRepository.FindByUsernameAsync(createUserDto.Username);
            if (existingUser != null) return false;
            var user = createUserDto.FromCreateUserDto();
            return await usersRepository.CreateAsync(user);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<UserDto>> FindAll()
    {
        try
        {
            var users = await usersRepository.FindAllAsync();
            return users.Select(user => user.ToUserDto()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    public async Task<UserDto?> FindById(int id)
    {
        try
        {
            var user = await usersRepository.FindByIdAsync(id);
            return user?.ToUserDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<Boolean> Update(int id, UpdateUserDto updateUserDto)
    {
        var user = updateUserDto.FromUpdateUserDto(id);
        return await usersRepository.UpdateAsync(user);

    }

    public async Task<Boolean> Delete(int id)
    {
        return await usersRepository.DeleteAsync(id);

    }
    public async Task<User?> FindByUsername(string username)
    {
        try
        {
            return await usersRepository.FindByUsernameAsync(username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}