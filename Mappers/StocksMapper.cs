namespace api.Mappers;
using Dtos.Stock;
using Models;

public static class StocksMapper
{
    public static StockDto ToStockDto(this Stock stock)
    {
        return new StockDto
        {
            Id = stock.Id,
            Symbol = stock.Symbol,
            CompanyName = stock.CompanyName,
            Purchase = stock.Purchase,
            LastDiv = stock.LastDiv,
            Industry = stock.Industry,
            MarketCap = stock.MarketCap,
            CreatedAt = stock.CreatedAt,
            Comments = stock.Comments.Select(comment => comment.ToCommentDto()).ToList()
        };
    }  
    public static Stock FromCreateStockDto(this CreateStockDto createStockDto)
    {
        return new Stock
        {
            Symbol = createStockDto.Symbol,
            CompanyName = createStockDto.CompanyName,
            Purchase = createStockDto.Purchase,
            LastDiv = createStockDto.LastDiv,
            Industry = createStockDto.Industry,
            MarketCap = createStockDto.MarketCap
        };
    } 
    public static Stock FromUpdateStockDto(this UpdateStockDto updateStockDto, int id)
    {
        return new Stock
        {
            Id = id,
            Symbol = updateStockDto.Symbol,
            CompanyName = updateStockDto.CompanyName,
            Purchase = updateStockDto.Purchase,
            LastDiv = updateStockDto.LastDiv,
            Industry = updateStockDto.Industry,
            MarketCap = updateStockDto.MarketCap
        };
    }
}