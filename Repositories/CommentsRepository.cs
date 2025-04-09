namespace api.Repositories;
using Context;
using Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public class CommentsRepository (AppDbContext context): ICommentsRepository
{
    public async Task<Boolean> CreateAsync(Comment comment)
    {
        try
        {
            await context.Comments.AddAsync(comment);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<List<Comment>> FindAllAsync()
    {
        try
        {
            return await context.Comments.ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Comment?> FindByIdAsync(int id)
    {
        return await context.Comments.FindAsync(id);
    }

    public async Task<Boolean> UpdateAsync(Comment commentToUpdate)
    {
        var foundComment = await context.Comments.FirstOrDefaultAsync(comment => comment.Id == commentToUpdate.Id);
        if (foundComment == null) return false;
        context.Entry(foundComment).CurrentValues.SetValues(commentToUpdate);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }

    public async Task<Boolean> DeleteAsync(int id)
    {
        var comment = await context.Comments.FirstOrDefaultAsync(comment => comment.Id == id);
        if (comment == null) return false;
        context.Comments.Remove(comment);
        var result = await context.SaveChangesAsync();
        return result > 0;
    }
}