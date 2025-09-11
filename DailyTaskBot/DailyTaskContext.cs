using System;
using Microsoft.EntityFrameworkCore;

namespace DailyTaskBot;

public class DailyTaskContext : DbContext
    {
        public DailyTaskContext(DbContextOptions<DailyTaskContext> options) : base(options) { }

        public DbSet<EmployeeDailyTask> EmployeeDailyTasks { get; set; }

        // Optional: configure table names / constraints
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDailyTask>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.EmployeeName).IsRequired();
                entity.Property(e => e.YesterdaysTask).HasMaxLength(500);
                entity.Property(e => e.TodaysTask).HasMaxLength(500);
                entity.Property(e => e.Obstacle).HasMaxLength(500);
                entity.Property(e => e.CreatedDate).IsRequired();
            });
        }
    }
