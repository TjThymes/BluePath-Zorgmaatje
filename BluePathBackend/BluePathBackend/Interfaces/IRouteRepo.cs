using BluePath_Backend.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePath_Backend.Interfaces
{
    public interface IRouteRepository
    {
        Task<IEnumerable<RouteStep>> GetStepsForRouteAsync(string routeType);
        Task<RouteStep> CreateAsync(RouteStep entry);
    }
}
