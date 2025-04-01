namespace api.Dtos.Stock;
using System.ComponentModel.DataAnnotations;
using Comment;

public class StockDto
{
    public int Id { get; set; }
    [MaxLength (10)]
    public string Symbol { get; set; } = string.Empty;
    [MaxLength (10)]
    public string CompanyName { get; set; } = string.Empty;
    
    public decimal Purchase { get; set; }
    
    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;
    
    public long MarketCap { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public List<CommentDto> Comments { get; set; } = new List<CommentDto>();

}