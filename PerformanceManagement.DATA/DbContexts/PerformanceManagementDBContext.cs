using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PerformanceManagement.ENTITIES;


namespace PerformanceManagement.DATA.DbContexts
{
    public class PerformanceManagementDBContext : IdentityDbContext<User, AppRole, int, IdentityUserClaim<int>, AppUserRole, IdentityUserLogin<int>,IdentityRoleClaim<int>, IdentityUserToken<int>>
    {
        public PerformanceManagementDBContext(DbContextOptions<PerformanceManagementDBContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder) {

            modelBuilder.Entity<SystemeUser>()
         .HasKey(Us => new { Us.SystemeId, Us.UserId });
            modelBuilder.Entity<SystemeUser>()
                .HasOne(Us => Us.User)
                .WithMany(u => u.SystemeUsers)
                .HasForeignKey(Us => Us.UserId);
            modelBuilder.Entity<SystemeUser>()
                .HasOne(Us => Us.Systeme)
                .WithMany(s => s.SystemeUsers)
                .HasForeignKey(Us => Us.SystemeId);
            modelBuilder.Entity<User>().HasIndex(s => s.Email).IsUnique();

           

            base.OnModelCreating(modelBuilder);


        }

        public DbSet<User> Employees { get; set; }
        public DbSet<Badge> Badges { get; set; }
        public DbSet<Systeme> Systemes { get; set; }
        public DbSet<UserBadge> userBadges { get; set; }
        public DbSet<VoteRights> VoteRights { get; set; }
        public DbSet<VoteHistory> VoteHistories { get; set; }
        public DbSet<Event> Events { get; set; }
        public DbSet<DayEvent> DayEvents { get; set; }





    }
}
