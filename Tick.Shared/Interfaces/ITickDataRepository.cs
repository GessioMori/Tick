using Tick.Models.Entities;

namespace Tick.Shared.Interfaces
{
    public interface ITickDataRepository
    {
        Task<IEnumerable<Habit>> GetAllHabitsByUserIdAsync(string userId);
        Task InsertHabitAsync(Habit habit);
        Task<bool> GetHabitByNameAndUserAsync(string userId, string habitName);
    }
}
