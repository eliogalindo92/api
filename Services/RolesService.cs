namespace api.Services;
using Mappers;
using Dtos.Roles;
using Interfaces;

public class RolesService(IRolesRepository rolesRepository, IPermissionsRepository permissionsRepository)
{
    public async Task<Boolean> Create(CreateRoleDto createRoleDto)
    {
        try
        {
            var existingRole = await rolesRepository.FindByDenominationAsync(createRoleDto.Denomination);
            if (existingRole != null) return false;
            var permissionsRelated = await permissionsRepository.FindAllByIdAsync(createRoleDto.Permissions);
            if(permissionsRelated.Count != createRoleDto.Permissions.Count) return false;
            var role = createRoleDto.FromCreateRoleDto(permissionsRelated);
            return await rolesRepository.CreateAsync(role);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<RoleDto>> FindAll()
    {
        try
        {
            var roles = await rolesRepository.FindAllAsync();
            return roles.Select(role => role.ToRoleDto()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }
    public async Task<RoleDto?> FindById(int id)
    {
        try
        {
            var role = await rolesRepository.FindByIdAsync(id);
            return role?.ToRoleDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

   public async Task<Boolean> Update(int id, UpdateRoleDto updateRoleDto)
{
    try
    {  
        var existingRole = await rolesRepository.FindByIdWithPermissionsAsync(id);
        
        if (existingRole is null) return false; 
        
        existingRole.Denomination = updateRoleDto.Denomination;
        existingRole.Description = updateRoleDto.Description;
        existingRole.Enabled = updateRoleDto.Enabled;
        
        var newPermissions = await permissionsRepository
            .FindAllByIdAsync(updateRoleDto.Permissions);
        
        if (newPermissions.Count != updateRoleDto.Permissions.Distinct().Count()) return false;
        existingRole.Permissions.Clear(); 
        foreach (var permission in newPermissions) 
        {
            existingRole.Permissions.Add(permission);
        }
        
        return await rolesRepository.UpdateAsync(existingRole); 
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
}
   
    public async Task<Boolean> Delete(int id)
    {
        try
        {
            return await rolesRepository.DeleteAsync(id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}