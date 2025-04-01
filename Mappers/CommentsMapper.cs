namespace api.Mappers;
using Dtos.Comment;
using Models;

public static class CommentsMapper
{
    public static CommentDto ToCommentDto(this Comment comment)
    {
        return new CommentDto
        {
            Id = comment.Id,
            Title = comment.Title,
            Content = comment.Content,
            CreatedAt = comment.CreatedAt,
        };
    }  
    public static Comment FromCreateCommentDto(this CreateCommentDto createCommentDto)
    {
        return new Comment
        {
            Title = createCommentDto.Title,
            Content = createCommentDto.Content,
            CreatedAt = createCommentDto.CreatedAt,
            StockId = createCommentDto.StockId,
        };
    }
    public static Comment FromUpdateCommentDto(this UpdateCommentDto updateCommentDto, int id)
    {
        return new Comment
        {
            Id = id,
            Title = updateCommentDto.Title,
            Content = updateCommentDto.Content,
            CreatedAt = updateCommentDto.CreatedAt,
            StockId = updateCommentDto.StockId,
        };
    }
}