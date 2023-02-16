using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using YTSummarizer.Auth.Models;
using YTSummarizer.Auth.Security;
using YTSummarizer.Auth.Services;

namespace YTSummarizer.Auth.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class DataController : ControllerBase
{
    private readonly IDataService _dataService;
    private readonly ISecurity _security;

    public DataController(IDataService dataService, ISecurity security)
    {
        _dataService = dataService;
        _security = security;
    }

    [HttpGet]
    public async Task<List<Data>> Get() =>
        await _dataService.GetAsync();

    [HttpGet("{id:length(24)}")]
    public async Task<ActionResult<Data>> Get(string id)
    {
        var data = await _dataService.GetAsync(id);

        if (data is null)
        {
            return NotFound();
        }

        return data;
    }

    [HttpGet("/user/{id}")]
    public async Task<ActionResult<List<Data>>> GetUserData(string id)
    {
        var userId = _security.GetUserIdFromAccessToken(Request);

        if (userId != id)
        {
            return Unauthorized("Not authorized to access the resource");
        }
        var data = await _dataService.GetByUserIdAsync(id);



        if (data is null)
        {
            return NotFound();
        }

        return data.ToList();
    }

    [HttpPost]
    public async Task<IActionResult> Post(Data data)
    {
        var userId = _security.GetUserIdFromAccessToken(Request);
        if (userId is null) return BadRequest("No userId");
        data.CreatedBy = userId;
        await _dataService.CreateAsync(data);

        return CreatedAtAction(nameof(Get), new { id = data.Id }, data);
    }

    [HttpPut("{id:length(24)}")]
    public async Task<IActionResult> Update(string id, Data updatedData)
    {
        var data = await _dataService.GetAsync(id);

        if (data is null)
        {
            return NotFound();
        }

        updatedData.Id = data.Id;

        await _dataService.UpdateAsync(id, updatedData);

        return NoContent();
    }

    [HttpDelete("{id:length(24)}")]
    public async Task<IActionResult> Delete(string id)
    {
        var data = await _dataService.GetAsync(id);

        if (data is null)
        {
            return NotFound();
        }

        await _dataService.RemoveAsync(id);

        return NoContent();
    }
}