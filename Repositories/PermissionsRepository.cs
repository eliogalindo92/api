namespace api.Repositories;
using Context;
using Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public class PermissionsRepository(AppDbContext context): IPermissionsRepository
{
    public async Task<Boolean> CreateAsync(Permission permission)
    {
        try
        {
            await context.Permissions.AddAsync(permission);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Permission>> FindAllAsync()
    {
        try
        {
            return await context.Permissions.Select(permission => new Permission
                {
                    Id = permission.Id,
                    Denomination = permission.Denomination,
                    Description = permission.Description
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Permission?> FindByIdAsync(int id)
    {
        try
        {
            return await context.Permissions.Where(permission => permission.Id == id)
                .Select(permission => new Permission 
                {
                    Id = permission.Id,
                    Denomination = permission.Denomination,
                    Description = permission.Description
                })
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> UpdateAsync(Permission permissionToUpdate)
    {
        try
        {
            var foundPermission = await context.Permissions.FirstOrDefaultAsync(p => p.Id == permissionToUpdate.Id);
            if (foundPermission is null) return false;
            context.Entry(foundPermission).CurrentValues.SetValues(permissionToUpdate);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        try
        {
            var permission = await context.Permissions.FirstOrDefaultAsync(p => p.Id == id);
            if (permission is null) return false;
            context.Permissions.Remove(permission);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<Permission>> FindAllByIdAsync(List<int> permissionIds)
    {
        return await context.Permissions
            .Where(permission => permissionIds.Contains(permission.Id))
            .ToListAsync();
    }
}