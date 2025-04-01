namespace api.Dtos.Comment;
using System.ComponentModel.DataAnnotations;

public class CreateCommentDto
{
    [Required]
    [MaxLength (100)]
    public string Title { get; set; } = string.Empty;
    [Required]
    [MaxLength (256)]
    public string Content { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    [Required]
    public int StockId { get; set; }
}