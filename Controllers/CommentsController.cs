namespace api.Controllers;
using Dtos.Comment;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class CommentsController(CommentsService commentsService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCommentDto createCommentDto)
    {
        var created = await commentsService.Create(createCommentDto);
        if (!created) return BadRequest();
        return Ok("Success");
    }
    
    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var comments = await commentsService.FindAll();
        return Ok(comments);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var comment = await commentsService.FindById(id);
        if (comment is null) return NotFound();
        return Ok(comment);
    }
    
    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateCommentDto updateCommentDto)
    {
        var updated = await commentsService.Update(id, updateCommentDto);
        if (!updated) return NotFound();
        return Ok("Success");
    } 
    
    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await commentsService.Delete(id);
        if (!deleted) return NotFound();
        return Ok("Success");
    }
}