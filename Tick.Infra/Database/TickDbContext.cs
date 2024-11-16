using Microsoft.EntityFrameworkCore;
using Tick.Models.Entities;

namespace Tick.Infra.Database
{
    public class TickDbContext : DbContext
    {
        public required DbSet<Habit> Habits { get; set; }
        public required DbSet<HabitCompletion> HabitCompletions { get; set; }

        public TickDbContext(DbContextOptions<TickDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Habit>()
                .HasMany(h => h.HabitCompletions)
                .WithOne(hc => hc.Habit)
                .HasForeignKey(hc => hc.HabitId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
