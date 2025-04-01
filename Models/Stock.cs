namespace api.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Stock
{
    public int Id { get; set; }
    [MaxLength (10)]
    public string Symbol { get; set; } = string.Empty;
    [MaxLength (10)]
    public string CompanyName { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18, 2)")]
    public decimal Purchase { get; set; }

    [Column(TypeName = "decimal(18, 2)")]
    public decimal LastDiv { get; set; }

    public string Industry { get; set; } = string.Empty;

    public long MarketCap { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    public List<Comment> Comments { get; set; } = new List<Comment>();

}