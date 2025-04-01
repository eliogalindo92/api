namespace api.Dtos.Comment;
using System.ComponentModel.DataAnnotations;


public class CommentDto
{
    public int Id { get; set; }
    [MaxLength (100)]
    public string Title { get; set; } = string.Empty;
    [MaxLength (256)]
    public string Content { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    
}