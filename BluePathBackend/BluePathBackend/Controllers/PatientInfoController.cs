using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;
using BluePathBackend.Objects;

namespace BluePathBackend.Controllers
{
    [ApiController]
    [Route("api/patientinfo")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class PatientInfoController : ControllerBase
    {
        private readonly IPatientInfoRepository _repo;
        private readonly ILogger<PatientInfoController> _logger;

        public PatientInfoController(IPatientInfoRepository repo, ILogger<PatientInfoController> logger)
        {
            _repo = repo;
            _logger = logger;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientInfo model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { Message = "Je bent niet correct ingelogd. (UserId ontbreekt in token)" });

            model.UserId = userId;

            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }

        [HttpGet("me")]
        public async Task<IActionResult> GetMyInfo()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrWhiteSpace(userId))
                return Unauthorized(new { Message = "Je bent niet correct ingelogd." });

            var info = await _repo.GetByUserIdAsync(userId);
            return info == null ? NotFound() : Ok(info);
        }

        [HttpGet("whoami")]
        public IActionResult WhoAmI()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            return Ok(new
            {
                IsAuthenticated = User.Identity?.IsAuthenticated,
                UserId = userId,
                Claims = User.Claims.Select(c => new { c.Type, c.Value })
            });
        }
    }
}
