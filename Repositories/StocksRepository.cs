namespace api.Repositories;
using Context;
using Interfaces;
using Models;
using Microsoft.EntityFrameworkCore;

public class StocksRepository(AppDbContext context): IStocksRepository
{
    public async Task<Boolean> CreateAsync(Stock stock)
    {
        try
        {
            await context.Stocks.AddAsync(stock);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    
    public async Task<List<Stock>> FindAllAsync()
    {
        try
        {
            return await context.Stocks.Include(stock => stock.Comments).ToListAsync();
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<Stock?> FindByIdAsync(int id)
    {
        try
        {
            return await context.Stocks.Include(stock=>stock.Comments).FirstOrDefaultAsync(stock => stock.Id == id);
            
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<Boolean> UpdateAsync(Stock stockToUpdate)
    {
        try
        {
           var foundStock = await context.Stocks.FirstOrDefaultAsync(stock => stock.Id == stockToUpdate.Id);
           if (foundStock is null) return false;
           context.Entry(foundStock).CurrentValues.SetValues(stockToUpdate);
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
            var stock = await context.Stocks.FirstOrDefaultAsync(stock => stock.Id == id);
            if (stock == null) return false;
            context.Stocks.Remove(stock);
            var result = await context.SaveChangesAsync();
            return result > 0;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}