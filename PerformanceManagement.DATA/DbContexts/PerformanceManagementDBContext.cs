﻿using Microsoft.EntityFrameworkCore;
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
           
            //many to many between user and badge
            modelBuilder.Entity<UserBadge>()
        .HasKey(ub => new { ub.UserId, ub.BadgeId });
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.User)
                .WithMany(u => u.UserBadges)
                .HasForeignKey(ub => ub.UserId );
            modelBuilder.Entity<UserBadge>()
                .HasOne(ub => ub.Badge)
                .WithMany(b => b.UserBadges)
                .HasForeignKey(ub => ub.BadgeId);


            //One To Many between User and User_System
            modelBuilder.Entity<User>()
        .HasMany(u => u.User_Systems)
        .WithOne(s => s.User);



        }

        public DbSet<User> Users { get; set; }
        public DbSet<Badge> Badges { get; set; }

    }
}
