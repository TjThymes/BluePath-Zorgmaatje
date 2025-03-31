using BluePathBackend.Objects;
using System;

namespace BluePath_Backend.Objects
{
    public class UserRouteStepProgress
    {
        public Guid Id { get; set; }

        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public Guid RouteStepId { get; set; }
        public RouteStep Step { get; set; }

        public string Status { get; set; } // "todo", "in_progress", "done"
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
