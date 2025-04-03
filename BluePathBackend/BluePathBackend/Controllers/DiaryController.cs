using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[ApiController]
[Route("api/diary")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class DiaryController : ControllerBase
{
    private readonly IDiaryRepository _repo;

    public DiaryController(IDiaryRepository repo)
    {
        _repo = repo;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DiaryEntry entry)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized("Invalid user token.");

        entry.UserId = userId;
        var result = await _repo.CreateAsync(entry);
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var entries = await _repo.GetAllForUserAsync(userId);
        return Ok(entries);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var entry = await _repo.GetByIdAsync(id);
        return entry == null ? NotFound() : Ok(entry);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DiaryEntry entry)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrWhiteSpace(userId))
            return Unauthorized("Invalid user token.");

        entry.UserId = userId;
        var result = await _repo.UpdateAsync(id, entry);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var success = await _repo.DeleteAsync(id, userId);
        return success ? NoContent() : NotFound();
    }
}
