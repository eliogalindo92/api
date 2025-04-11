namespace api.Mappers;
using Dtos.Permissions;
using Dtos.Roles;
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
            Roles = user.Roles.Select(role => new RoleDto
            {
                Id = role.Id,
                Denomination = role.Denomination,
                Description = role.Description,
                Enabled = role.Enabled,
                CreatedAt = role.CreatedAt,
                Permissions = role.Permissions.Select(permission => new PermissionDto
                {
                    Id = permission.Id,
                    Denomination = permission.Denomination,
                    Description = permission.Description,
                    CreatedAt = permission.CreatedAt,
                }).ToList()
            }).ToList(),
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
}