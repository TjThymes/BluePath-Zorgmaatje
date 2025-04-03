using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BluePathBackend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/progress")]
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

            var result = await _repo.UpdateProgressAsync(userId, stepId, status);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMine()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var result = await _repo.GetProgressForUserAsync(userId);
            return Ok(result);
        }
    }
}
