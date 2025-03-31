using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

[Authorize]
[ApiController]
[Route("api/avatar")]
public class AvatarController : ControllerBase
{
    private readonly IUserAvatarRepository _repo;

    public AvatarController(IUserAvatarRepository repo)
    {
        _repo = repo;
    }

    [HttpPost("set")]
    public async Task<IActionResult> SetAvatar([FromBody] UserAvatar model)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var result = await _repo.SetOrUpdateAsync(userId, model);
        return Ok(result);
    }

    [HttpGet("me")]
    public async Task<IActionResult> GetMyAvatar()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var avatar = await _repo.GetByUserIdAsync(userId);
        if (avatar == null)
            return NotFound();

        return Ok(avatar);
    }

}

