namespace api.Dtos.Stock;
using System.ComponentModel.DataAnnotations;

public class UpdateStockDto
{
    [MaxLength (10)]
    public string Symbol { get; set; } = string.Empty;
    [MaxLength (10)]
    public string CompanyName { get; set; } = string.Empty;
    public decimal Purchase { get; set; }
    
    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }
}