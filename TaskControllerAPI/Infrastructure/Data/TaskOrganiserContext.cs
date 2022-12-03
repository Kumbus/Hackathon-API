using Domain.Common;
using Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class TaskOrganiserContext : IdentityDbContext<User>
    {
        public DbSet<User> Users { get; set; }
        public DbSet<ActivitySlot> ActivitySlots { get; set; }
        public DbSet<PlannedTask> Tasks { get; set; }

        public DbSet<Identificator> Identificators { get; set; }

        public TaskOrganiserContext(DbContextOptions<TaskOrganiserContext> options) : base(options)
        {

        }

        public async Task<int> SaveChangesAsync()
        {
            var entries = ChangeTracker
                .Entries()
                .Where(e => e.Entity is EntityBase && (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                ((EntityBase)entityEntry.Entity).LastModified = DateTime.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((EntityBase)entityEntry.Entity).CreatedAt= DateTime.UtcNow;
                }
            }
            return await base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .ToTable("Users");

            modelBuilder.Entity<PlannedTask>()
                .ToTable("Tasks");

            modelBuilder.Entity<ActivitySlot>()
                .ToTable("Activity Slots")
                .Property(s => s.CategoryOfActivity)
                .HasConversion<string>();               

            base.OnModelCreating(modelBuilder);

        }
    }
}
