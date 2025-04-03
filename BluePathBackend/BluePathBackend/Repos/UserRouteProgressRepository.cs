using BluePath_Backend.Data;
using BluePath_Backend.Interfaces;
using BluePath_Backend.Objects;
using Microsoft.EntityFrameworkCore;

namespace BluePathBackend.Repos
{
    public class UserRouteProgressRepository : IUserRouteProgressRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRouteProgressRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserRouteStepProgress> UpdateProgressAsync(string userId, Guid stepId, string status)
        {
            var progress = await _context.StepProgress
                .FirstOrDefaultAsync(p => p.UserId == userId && p.RouteStepId == stepId);

            if (progress == null)
            {
                progress = new UserRouteStepProgress
                {
                    UserId = userId,
                    RouteStepId = stepId,
                    Status = status
                };
                _context.StepProgress.Add(progress);
            }
            else
            {
                progress.Status = status;
                progress.UpdatedAt = DateTime.UtcNow;
            }

            await _context.SaveChangesAsync();
            return progress;
        }

        public async Task<IEnumerable<UserRouteStepProgress>> GetProgressForUserAsync(string userId)
        {
            return await _context.StepProgress
                .Include(p => p.Step)
                .Where(p => p.UserId == userId)
                .ToListAsync();
        }
    }
}
