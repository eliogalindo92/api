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
            return await context.Users.ToListAsync();
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
            return await context.Users.FindAsync(id);

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
            var foundUser = await context.Users.FirstOrDefaultAsync(user => user.Id == userToUpdate.Id);
            if (foundUser is null) return false;
            context.Entry(foundUser).CurrentValues.SetValues(userToUpdate);
            await context.SaveChangesAsync();
            return true;
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
            await context.SaveChangesAsync();
            return true;
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
            return await context.Users.FirstOrDefaultAsync(user => user.Username == username);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}