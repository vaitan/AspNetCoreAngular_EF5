using AspNetCoreAngular_EF5.Models;
using Microsoft.EntityFrameworkCore;

namespace AspNetCoreAngular_EF5.Persistence
{
    public class NewDbContext : DbContext
    {
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Make> Makes { get; set; }
        public DbSet<Feature> Features { get; set; }

        public NewDbContext(DbContextOptions<NewDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeature>()
                .HasKey(vf => new { vf.VehicleId, vf.FeatureId });

            base.OnModelCreating(modelBuilder);
        }
    }
}