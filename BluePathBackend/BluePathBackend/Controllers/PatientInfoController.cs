using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using System.Security.Claims;
using BluePathBackend.Objects;

namespace BluePath_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/patientinfo")]
    public class PatientInfoController : ControllerBase
    {
        private readonly IPatientInfoRepository _repo;

        public PatientInfoController(IPatientInfoRepository repo)
        {
            _repo = repo;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientInfo model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            model.UserId = userId;

            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var info = await _repo.GetByUserIdAsync(userId);

            if (info == null)
                return NotFound();

            return Ok(info);
        }
    }
}
