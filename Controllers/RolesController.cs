namespace api.Controllers;
using Dtos.Roles;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize]
[ApiController]
[Route("[controller]")]
public class RolesController(RolesService rolesService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRoleDto createRoleDto)
    {
        var created = await rolesService.Create(createRoleDto);
        if (!created) return BadRequest();
        return Ok("Success");
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var stocks = await rolesService.FindAll();
        return Ok(stocks);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var stock = await rolesService.FindById(id);
        if (stock is null) return NotFound();
        return Ok(stock);
    }
    
    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateRoleDto updateRoleDto)
    {
        var updated = await rolesService.Update(id, updateRoleDto);
        if (!updated) return NotFound();
        return Ok("Success");
    } 
    
    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await rolesService.Delete(id);
        if (!deleted) return NotFound();
        return Ok("Success");
    }
}