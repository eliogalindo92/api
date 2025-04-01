namespace api.Services;
using Dtos.Comment;
using Interfaces;
using Mappers;

public class CommentsService(ICommentsRepository commentsRepository, StocksService stocksService)
{
    public async Task<Boolean> Create(CreateCommentDto createCommentDto)
    {
        try
        {
            if(!await stocksService.Exists(createCommentDto.StockId)) return false;
            var comment = createCommentDto.FromCreateCommentDto();
            return await commentsRepository.CreateAsync(comment);

        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    public async Task<List<CommentDto>> FindAll()
    {
        try
        {
            var comments = await commentsRepository.FindAllAsync();
            return comments.Select(comment => comment.ToCommentDto()).ToList();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    public async Task<CommentDto?> FindById(int id)
    {
        try
        {
            var comment = await commentsRepository.FindByIdAsync(id);
            return comment?.ToCommentDto();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
        
    }
    
    public async Task<Boolean> Update(int id, UpdateCommentDto updateCommentDto)
    {
        var comment = updateCommentDto.FromUpdateCommentDto(id);
        return await commentsRepository.UpdateAsync(comment);

    }
    
    public async Task<Boolean> Delete(int id)
    {
        return await commentsRepository.DeleteAsync(id);

    }
}