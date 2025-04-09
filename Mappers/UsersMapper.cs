namespace api.Mappers;
using Dtos.User;
using Models;

public static class UsersMapper
{
    public static UserDto ToUserDto(this User user)
    {
        return new UserDto
        {
            Id = user.Id,
            FullName = user.FullName,
            Username = user.Username,
            Email = user.Email,
            Status = user.Status,
            CreatedAt = user.CreatedAt,
            Roles = user.Roles
        };
    }  
    public static User FromCreateUserDto(this CreateUserDto createUserDto, List<Role> roles)
    {
        return new User
        {
            FullName = createUserDto.FullName,
            Username = createUserDto.Username,
            Email = createUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(createUserDto.Password),
            Status = createUserDto.Status,
            Roles = roles,
        };
    }
    public static User FromUpdateUserDto(this UpdateUserDto updateUserDto, int id)
    {
        return new User
        {
            Id = id,
            FullName = updateUserDto.FullName,
            Username = updateUserDto.Username,
            Email = updateUserDto.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(updateUserDto.Password),
            Status = updateUserDto.Status,
        };
    }
}