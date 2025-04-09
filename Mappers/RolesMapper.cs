namespace api.Mappers;
using Dtos.Roles;
using Models;

public static class RolesMapper
{
    public static RoleDto ToRoleDto(this Role role)
    {
        return new RoleDto
        {
            Id = role.Id,
            Denomination = role.Denomination,
            Description = role.Description,
            Enabled = role.Enabled,
            CreatedAt = role.CreatedAt,
            Permissions = role.Permissions
        };
    }
    public static Role FromCreateRoleDto(this CreateRoleDto createRoleDto, List<Permission> permissions)
    {
        return new Role
        {
            Denomination = createRoleDto.Denomination,
            Description = createRoleDto.Description,
            Enabled = createRoleDto.Enabled,
            Permissions = permissions,
        };
    }
    public static Role FromUpdateRoleDto(this UpdateRoleDto updateRoleDto, List<Permission> permissions, int id)
    {
        return new Role
        {
            Id = id,
            Denomination = updateRoleDto.Denomination,
            Description = updateRoleDto.Description,
            Enabled = updateRoleDto.Enabled,
            Permissions = permissions,
        };
    }
}