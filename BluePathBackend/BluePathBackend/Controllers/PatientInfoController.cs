using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using System.Security.Claims;
using BluePathBackend.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace BluePathBackend.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [ApiController]
    [Route("api/patientinfo")]
    public class PatientInfoController : ControllerBase
    {
        private readonly IPatientInfoRepository _repo;
        private readonly ILogger<PatientInfoController> _logger;

        public PatientInfoController(IPatientInfoRepository repo, ILogger<PatientInfoController> logger)
        {
            _repo = repo;
            _logger = logger;
            _logger.LogInformation("🔧 PatientInfoController is geladen");
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientInfo model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Je bent niet correct ingelogd. (UserId ontbreekt in token)" });
            }

            model.UserId = userId;

            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized(new { Message = "Je bent niet correct ingelogd." });
            }

            var info = await _repo.GetByUserIdAsync(userId);

            if (info == null)
                return NotFound();

            return Ok(info);
        }
        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var claims = User.Claims.Select(c => new { c.Type, c.Value });
            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated,
                NameIdentifier = User.FindFirstValue(ClaimTypes.NameIdentifier),
                Claims = claims
            });
        }
    }
}
