namespace api.Controllers;
using Dtos.User;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController(UsersService usersService): ControllerBase
{
    [Authorize(Policy = "RequireCreateUsers")]
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateUserDto createUserDto)
    {
        var created = await usersService.Create(createUserDto);
        if (!created) return BadRequest();
        return Ok("Success");
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var users = await usersService.FindAll();
        return Ok(users);
    }

    [HttpGet("{Id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var user = await usersService.FindById(id);
        if (user is null) return NotFound();
        return Ok(user);
    }

    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
    {
        var updated = await usersService.Update(id, updateUserDto);
        if (!updated) return NotFound();
        return Ok("Success");
    }

    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await usersService.Delete(id);
        if (!deleted) return NotFound();
        return Ok("Success");
    }
}