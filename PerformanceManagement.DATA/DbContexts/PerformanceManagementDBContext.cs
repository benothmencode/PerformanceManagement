using Microsoft.EntityFrameworkCore;
using PerformanceManagement.ENTITIES;


namespace PerformanceManagement.DATA.DbContexts
{
    public class PerformanceManagementDBContext : DbContext
    {
        public PerformanceManagementDBContext(DbContextOptions<PerformanceManagementDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<User>().HasIndex(s => s.Username).IsUnique();
            modelBuilder.Entity<User>().HasIndex(s => s.Email).IsUnique();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Badge> Badges { get; set; }

        public DbSet<Resource> Resources { get; set; }

        public DbSet<Resource_Parameter> ResourceParameters { get; set; }

        public DbSet<Resource_URI> ResourcesURIS { get; set; }


    }
}
