using BikeManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace BikeManagement.Data.Context
{
    public class BikeDbContext : DbContext
    {
        public DbSet<Bike> Bikes { get; set; }

        public BikeDbContext(DbContextOptions<BikeDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Bike>()
                .HasKey(b => b.Id);

            base.OnModelCreating(modelBuilder);
        }
    }
}
