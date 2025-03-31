using BluePath_Backend.Objects;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BluePath_Backend.Interfaces
{
    public interface IUserRouteProgressRepository
    {
        Task<UserRouteStepProgress> UpdateProgressAsync(string userId, Guid stepId, string status);
        Task<IEnumerable<UserRouteStepProgress>> GetProgressForUserAsync(string userId);
    }
}
