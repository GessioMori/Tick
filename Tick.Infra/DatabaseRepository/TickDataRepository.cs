using Microsoft.EntityFrameworkCore;
using Tick.Infra.Database;
using Tick.Models.Entities;
using Tick.Shared.Interfaces;

namespace Tick.Infra.DatabaseRepository
{
    public class TickDataRepository : ITickDataRepository
    {
        private readonly TickDbContext _tickDbContext;
        private readonly DbSet<Habit> _dbSetHabit;
        private readonly DbSet<HabitCompletion> _dbSetHabitCompletion;

        public TickDataRepository(TickDbContext tickDbContext)
        {
            this._tickDbContext = tickDbContext;
            this._dbSetHabit = tickDbContext.Habits;
            this._dbSetHabitCompletion = tickDbContext.HabitCompletions;
        }

        public async Task<IEnumerable<Habit>> GetAllHabitsByUserIdAsync(string userId)
        {
            return await this._dbSetHabit.Where(h => h.UserId == userId).ToListAsync();
        }

        public async Task<IEnumerable<Habit>> GetAllHabitsWithCompletionsByUserIdAsync(string userId)
        {
            return await this._dbSetHabit.Where(h => h.UserId == userId)
                .Include(h => h.HabitCompletions)
                .ToListAsync();
        }

        public async Task<Habit?> GetHabitByIdAsync(string habitId)
        {
            return await this._dbSetHabit.SingleOrDefaultAsync(h => h.Id == habitId);
        }

        public async Task<bool> GetHabitByNameAndUserAsync(string userId, string habitName)
        {
            return await this._dbSetHabit.AnyAsync(h => h.UserId == userId && h.Name == habitName);
        }

        public async Task InsertHabitAsync(Habit habit)
        {
            try
            {
                await this._dbSetHabit.AddAsync(habit);
                await _tickDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the habit.", ex);
            }
        }

        public async Task InsertHabitCompletionAsync(HabitCompletion habitCompletion)
        {
            try
            {
                await this._dbSetHabitCompletion.AddAsync(habitCompletion);
                await _tickDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the habit completion.", ex);
            }
        }
    }
}
