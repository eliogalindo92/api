namespace api.Repositories;
using Context;
using Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public class RolesRepository(AppDbContext context): IRolesRepository
{
    public async Task<Boolean> CreateAsync(Role role)
    {
        try
        {
            await context.Roles.AddAsync(role);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Role>> FindAllAsync()
    {
        try
        {
            return await context.Roles.Select(role => new Role
                {
                    Id = role.Id,
                    Denomination = role.Denomination,
                    Description = role.Description,
                    Enabled = role.Enabled,
                    CreatedAt = role.CreatedAt,
                    Permissions = role.Permissions.Select(permission => new Permission
                    {
                        Id = permission.Id,
                        Denomination = permission.Denomination,
                        Description = permission.Description
                    }).ToList()
                })
                .ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Role?> FindByIdAsync(int id)
    {
        try
        {
            return await context.Roles.Where(role => role.Id == id)
                .Select(role => new Role
            {
                Id = role.Id,
                Denomination = role.Denomination,
                Description = role.Description,
                Enabled = role.Enabled,
                CreatedAt = role.CreatedAt,
                Permissions = role.Permissions.Select(permission => new Permission
                {
                    Id = permission.Id,
                    Denomination = permission.Denomination,
                    Description = permission.Description
                }).ToList()
            }).FirstOrDefaultAsync();

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> UpdateAsync(Role roleToUpdate)
    {
        try
        {
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
            var foundRole = await context.Roles.FirstOrDefaultAsync(role => role.Id == id);
            if (foundRole is null) return false;
            context.Roles.Remove(foundRole);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Role?> FindByDenominationAsync(string denomination)
    {
        try
        {
            return await context.Roles.FirstOrDefaultAsync(role => role.Denomination == denomination);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Role>> FindAllByIdAsync(List<int> roleIds)
    {
        return await context.Roles
            .Where(role => roleIds.Contains(role.Id))
            .ToListAsync();
    }
    
    public async Task<Role?> FindByIdWithPermissionsAsync(int id) 
    {
        try
        {
            return await context.Roles
                .Include(role => role.Permissions)
                .FirstOrDefaultAsync(role => role.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}