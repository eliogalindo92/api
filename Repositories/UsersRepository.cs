namespace api.Repositories;
using Context;
using Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public class UsersRepository(AppDbContext context): IUsersRepository
{
    public async Task<Boolean> CreateAsync(User user)
    {
        try
        {
            await context.Users.AddAsync(user);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<User>> FindAllAsync()
    {
        try
        {
            return await context.Users.Select(user => new User
            {
                Id = user.Id,
                FullName = user.FullName,
                Username = user.Username,
                Email = user.Email,
                Status = user.Status,
                CreatedAt = user.CreatedAt,
                Roles = user.Roles.Select(role => new Role
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
                        Description = permission.Description,
                        CreatedAt = permission.CreatedAt
                    }).ToList()
                }).ToList()
            }).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> FindByIdAsync(int id)
    {
        try
        {
            return await context.Users.Where(user => user.Id == id)
                .Select(user => new User
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Email = user.Email,
                    Status = user.Status,
                    CreatedAt = user.CreatedAt,
                    Roles = user.Roles.Select(role => new Role
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
                            Description = permission.Description,
                            CreatedAt = permission.CreatedAt
                        }).ToList()
                    }).ToList()
                }).FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Boolean> UpdateAsync(User userToUpdate)
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
            var user = await context.Users.FirstOrDefaultAsync(user => user.Id == id);
            if (user is null) return false;
            context.Users.Remove(user);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> FindByUsernameAsync(string username)
    {
        try
        {
            return await context.Users
                .Where(user => user.Username == username)
                .Select(user => new User
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Username = user.Username,
                    Email = user.Email,
                    Status = user.Status,
                    CreatedAt = user.CreatedAt,
                    Password = user.Password,
                    Roles = user.Roles.Select(role => new Role
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
                            Description = permission.Description,
                            CreatedAt = permission.CreatedAt
                        }).ToList()
                    }).ToList()
                })
                .FirstOrDefaultAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<User?> FindByIdWithRolesAsync(int id)
    {
        try
        {
            return await context.Users
                .Include(user => user.Roles)
                .FirstOrDefaultAsync(user => user.Id == id);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}