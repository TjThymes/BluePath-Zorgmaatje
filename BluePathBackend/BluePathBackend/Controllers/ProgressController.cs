using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BluePath_Backend.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BluePathBackend.Controllers
{
    [ApiController]
    [Route("api/progress")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class ProgressController : ControllerBase
    {
        private readonly IUserRouteProgressRepository _repo;

        public ProgressController(IUserRouteProgressRepository repo)
        {
            _repo = repo;
        }

        [HttpPost("{stepId}")]
        public async Task<IActionResult> Update(Guid stepId, [FromQuery] string status)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { Message = "Je bent niet correct ingelogd." });

            var result = await _repo.UpdateProgressAsync(userId, stepId, status);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { Message = "Je bent niet correct ingelogd." });

            var result = await _repo.GetProgressForUserAsync(userId);
            return Ok(result);
        }
    }
}
