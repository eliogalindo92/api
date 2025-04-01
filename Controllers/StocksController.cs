namespace api.Controllers;
using Dtos.Stock;
using Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;


[Authorize]
[ApiController]
[Route("[controller]")]
public class StocksController(StocksService stocksService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateStockDto createStockDto)
    {
        var created = await stocksService.Create(createStockDto);
        if (!created) return BadRequest();
        return Ok("Success");
    }

    [HttpGet]
    public async Task<IActionResult> FindAll()
    {
         var stocks = await stocksService.FindAll();
         return Ok(stocks);
    }
    [HttpGet("{Id:int}")]
    public async Task<IActionResult> FindById([FromRoute] int id)
    {
        var stock = await stocksService.FindById(id);
        if (stock is null) return NotFound();
        return Ok(stock);
    }
    
    [HttpPut("{Id:int}")]
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateStockDto updateStockDto)
    {
        var updated = await stocksService.Update(id, updateStockDto);
        if (!updated) return NotFound();
        return Ok("Success");
    } 
    
    [HttpDelete("{Id:int}")]
    public async Task<IActionResult> Delete([FromRoute] int id)
    {
        var deleted = await stocksService.Delete(id);
        if (!deleted) return NotFound();
        return Ok("Success");
    }
}