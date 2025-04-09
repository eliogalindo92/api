namespace api.Services;
using Dtos.Permissions;
using Interfaces;
using Mappers;

public class PermissionsService(IPermissionsRepository permissionsRepository)
{
    public async Task<Boolean> Create(CreatePermissionDto createPermissionDto)
    {
        try
        { 
            var permission = createPermissionDto.FromCreatePermissionDto();
            return await permissionsRepository.CreateAsync(permission);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<List<PermissionDto>> FindAll()
    {
        try
        {
            var permissions = await permissionsRepository.FindAllAsync();
            return permissions.Select(permission => permission.ToPermissionDto()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<PermissionDto?> FindById(int id)
    {
        try
        {
            var permission = await permissionsRepository.FindByIdAsync(id);
            return permission?.ToPermissionDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<Boolean> Update(int id, UpdatePermissionDto updatePermissionDto)
    {
        var permission = updatePermissionDto.FromUpdatePermissionDto(id);
        return await permissionsRepository.UpdateAsync(permission);
    }
    
    public async Task<Boolean> Delete(int id)
    {
        return await permissionsRepository.DeleteAsync(id);
    }
    
    public async Task<Boolean> Exists(int id)
    {
        try
        { 
            var permissionExists = await permissionsRepository.FindByIdAsync(id);
            return permissionExists != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}