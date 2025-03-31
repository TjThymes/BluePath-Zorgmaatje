using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BluePath_Backend.Interfaces;
using BluePathBackend.Objects;
using System.Security.Claims;
using BluePath_Backend.Objects;

namespace BluePath_Backend.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/routes")]
    public class RouteController : ControllerBase
    {
        private readonly IRouteRepository _repo;

        public RouteController(IRouteRepository repo)
        {
            _repo = repo;
        }

        [HttpGet("{routeType}")]
        public async Task<IActionResult> GetSteps(string routeType)
        {
            if (routeType != "A" && routeType != "B")
                return BadRequest("Ongeldige route. Kies 'A' of 'B'.");

            var steps = await _repo.GetStepsForRouteAsync(routeType);
            return Ok(steps);
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] RouteStep model)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _repo.CreateAsync(model);
            return Ok(result);
        }
    }
}
