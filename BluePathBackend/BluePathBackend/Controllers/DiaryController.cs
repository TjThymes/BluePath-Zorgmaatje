using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using System.Security.Claims;

namespace BluePath_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/diary")]
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var success = await _repo.DeleteAsync(id, userId);
            return success ? NoContent() : NotFound();
        }
    }
}
