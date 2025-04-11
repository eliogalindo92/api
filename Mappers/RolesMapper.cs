using api.Dtos.Permissions;

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
            Permissions = role.Permissions.Select(permission => new PermissionDto
            {
                Id = permission.Id,
                Denomination = permission.Denomination,
                Description = permission.Description,
                CreatedAt = permission.CreatedAt,
            }).ToList()
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
}