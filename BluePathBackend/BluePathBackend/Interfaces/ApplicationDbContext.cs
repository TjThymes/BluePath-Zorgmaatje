using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BluePathBackend.Objects;
using BluePath_Backend.Objects;

namespace BluePath_Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }


        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }
        public DbSet<RouteStep> RouteSteps { get; set; }
        public DbSet<UserRouteStepProgress> StepProgress { get; set; }
        public DbSet<UserAvatar> Avatars { get; set; }

    }
}
