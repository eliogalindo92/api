namespace api.Services;
using Dtos.Stock;
using Interfaces;
using Mappers;

public class StocksService(IStocksRepository stocksRepository)
{
    public async Task<Boolean> Create(CreateStockDto createStockDto)
    {
        try
        { 
            var stock = createStockDto.FromCreateStockDto();
            return await stocksRepository.CreateAsync(stock);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<List<StockDto>> FindAll()
    {
        try
        {
            var stocks = await stocksRepository.FindAllAsync();
            return stocks.Select(stock => stock.ToStockDto()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }

    public async Task<StockDto?> FindById(int id)
    {
        try
        {
            var stock = await stocksRepository.FindByIdAsync(id);
            return stock?.ToStockDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<Boolean> Update(int id, UpdateStockDto updateStockDto)
    {
        var stock = updateStockDto.FromUpdateStockDto(id);
        return await stocksRepository.UpdateAsync(stock);
    }
    
    public async Task<Boolean> Delete(int id)
    {
        return await stocksRepository.DeleteAsync(id);
    }
    
    public async Task<Boolean> Exists(int id)
    {
        try
        { 
            var stockExists = await stocksRepository.FindByIdAsync(id);
            return stockExists != null;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}