using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using System.Security.Claims;

namespace BluePathBackend.Controllers
{
    [ApiController]
    [Route("api/routes")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _repo;

        public RouteController(IRouteRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Haalt alle stappen op voor een route van type 'A' of 'B'.
        /// </summary>
        [HttpGet("{routeType}")]
        public async Task<IActionResult> GetSteps(string routeType)
        {
            if (string.IsNullOrWhiteSpace(routeType) || (routeType != "A" && routeType != "B"))
                return BadRequest(new { Message = "Ongeldige route. Kies 'A' of 'B'." });

            var steps = await _repo.GetStepsForRouteAsync(routeType);
            return Ok(steps);
        }

        /// <summary>
        /// Maakt een nieuwe route stap aan.
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] RouteStep model)
        {
            // Optional: Add user ownership here if necessary.
            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }
    }
}
