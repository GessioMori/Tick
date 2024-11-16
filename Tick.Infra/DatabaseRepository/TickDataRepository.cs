using Microsoft.EntityFrameworkCore;
using Tick.Infra.Database;
using Tick.Models.Entities;
using Tick.Shared.Interfaces;

namespace Tick.Infra.DatabaseRepository
{
    public class TickDataRepository : ITickDataRepository
    {
        private readonly TickDbContext _tickDbContext;
        private readonly DbSet<Habit> _dbSet;

        public TickDataRepository(TickDbContext tickDbContext)
        {
            this._tickDbContext = tickDbContext;
            this._dbSet = tickDbContext.Habits;
        }

        public async Task<IEnumerable<Habit>> GetAllHabitsByUserIdAsync(string userId)
        {
            return await this._dbSet.Where(h => h.UserId == userId).ToListAsync();
        }

        public async Task<bool> GetHabitByNameAndUserAsync(string userId, string habitName)
        {
            return await this._dbSet.AnyAsync(h => h.UserId == userId && h.Name == habitName);
        }

        public async Task InsertHabitAsync(Habit habit)
        {
            try
            {
                await this._dbSet.AddAsync(habit);
                await _tickDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("An error occurred while saving the habit.", ex);
            }
        }
    }
}
