namespace api.Services;
using Dtos.User;
using Interfaces;
using Mappers;
using Models;

public class UsersService(IUsersRepository usersRepository, IRolesRepository rolesRepository)
{
    public async Task<Boolean> Create(CreateUserDto createUserDto)
    {
        try
        {
            var existingUser = await usersRepository.FindByUsernameAsync(createUserDto.Username);
            if (existingUser != null) return false;
            var rolesRelated = await rolesRepository.FindAllByIdAsync(createUserDto.Roles);
            if (rolesRelated.Count != createUserDto.Roles.Count) return false;
            var user = createUserDto.FromCreateUserDto(rolesRelated);
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
        try
        {  
            var existingUser = await usersRepository.FindByIdWithRolesAsync(id);
        
            if (existingUser is null) return false; 
        
            existingUser.FullName = updateUserDto.FullName;
            existingUser.Username = updateUserDto.Username;
            existingUser.Email = updateUserDto.Email;
            existingUser.Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password);
            existingUser.Status = updateUserDto.Status;
        
            var newRoles = await rolesRepository
                .FindAllByIdAsync(updateUserDto.Roles);
        
            if (newRoles.Count != updateUserDto.Roles.Distinct().Count()) return false;
            existingUser.Roles.Clear(); 
            foreach (var role in newRoles) 
            {
                existingUser.Roles.Add(role);
            }
            return await usersRepository.UpdateAsync(existingUser); 
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> Delete(int id)
    {
        return await usersRepository.DeleteAsync(id);

    }
    public async Task<User?> FindByUsername(string username)
    {
        try
        {
            var user = await usersRepository.FindByUsernameAsync(username);
            if (user is null) return null;
            return user;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}