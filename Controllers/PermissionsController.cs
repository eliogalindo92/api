namespace api.Controllers;
using Dtos.Permissions;
using Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


[Authorize]
[ApiController]
[Route("[controller]")]
public class PermissionsController(PermissionsService permissionsService): ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreatePermissionDto createPermissionDto)
    {
        var created = await permissionsService.Create(createPermissionDto);
        if (!created) return BadRequest();
        return Ok("Success");
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
        var stocks = await permissionsService.FindAll();
        return Ok(stocks);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var stock = await permissionsService.FindById(id);
        if (stock is null) return NotFound();
        return Ok(stock);
    }
    
    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePermissionDto updatePermissionDto)
    {
        var updated = await permissionsService.Update(id, updatePermissionDto);
        if (!updated) return NotFound();
        return Ok("Success");
    } 
    
    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await permissionsService.Delete(id);
        if (!deleted) return NotFound();
        return Ok("Success");
    }
}