using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BluePathBackend.Objects;

namespace BluePath_Backend.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        // Voeg hier eventueel je DbSets toe, zoals:
        // public DbSet<Environment2D> Environments { get; set; }
        // public DbSet<GameObject> GameObjects { get; set; }
    }
}
