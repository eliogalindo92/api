namespace api.Mappers;
using Dtos.Permissions;
using Models;



public static class PermissionsMapper
{
    public static PermissionDto ToPermissionDto(this Permission permission)
    {
        return new PermissionDto
        {
            Id = permission.Id,
            Denomination = permission.Denomination,
            Description = permission.Description,
            CreatedAt = permission.CreatedAt,
        };
    }
    
    public static Permission FromCreatePermissionDto(this CreatePermissionDto createPermissionDto)
    {
        return new Permission
        {
            Denomination = createPermissionDto.Denomination,
            Description = createPermissionDto.Description,
        };
    }
    public static Permission FromUpdatePermissionDto(this UpdatePermissionDto updatePermissionDto, int id)
    {
        return new Permission
        {
            Id = id,
            Denomination = updatePermissionDto.Denomination,
            Description = updatePermissionDto.Description,
        };
    }
}