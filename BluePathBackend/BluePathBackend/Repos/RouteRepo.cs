using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.EntityFrameworkCore;

namespace BluePathBackend.Repos
{
    public class RouteRepository : IRouteRepository
    {
        private readonly ApplicationDbContext _context;

        public RouteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RouteStep>> GetStepsForRouteAsync(string routeType)
        {
            return await _context.RouteSteps
                .Where(r => r.RouteType == routeType)
                .OrderBy(r => r.StepOrder)
                .ToListAsync();
        }
        public async Task<RouteStep> CreateAsync(RouteStep step)
        {
            _context.RouteSteps.Add(step);
            await _context.SaveChangesAsync();
            return step;
        }
    }
}
