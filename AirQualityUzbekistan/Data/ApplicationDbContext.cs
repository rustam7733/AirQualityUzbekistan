using AirQualityUzbekistan.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace AirQualityUzbekistan.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<AirQualityRecord> AirQualityRecords { get; set; }
        public DbSet<LocationState> LocationStates { get; set; }
        public DbSet<LocationCity> LocationCities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<LocationCity>()
                .HasOne(c => c.LocationState)
                .WithMany(s => s.Cities)
                .HasForeignKey(c => c.LocationStateId);
        }
    }
}