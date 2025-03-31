using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BluePathBackend.Objects;
using BluePath_Backend.Objects;

namespace BluePath_Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Voeg hier eventueel je DbSets toe, zoals:
        public DbSet<PatientInfo> PatientInfos { get; set; }
        public DbSet<DiaryEntry> DiaryEntries { get; set; }


        // public DbSet<Environment2D> Environments { get; set; }
        // public DbSet<GameObject> GameObjects { get; set; }
    }
}
