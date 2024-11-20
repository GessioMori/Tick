using Tick.Models.Entities;

namespace Tick.Shared.Interfaces
{
    public interface ITickDataRepository
    {
        Task<IEnumerable<Habit>> GetAllHabitsByUserIdAsync(string userId);
        Task<IEnumerable<Habit>> GetAllHabitsWithCompletionsByUserIdAsync(string userId);
        Task<bool> GetHabitByNameAndUserAsync(string userId, string habitName);
        Task<Habit?> GetHabitByIdAsync(string habitId);
        Task InsertHabitAsync(Habit habit);
        Task InsertHabitCompletionAsync(HabitCompletion habitCompletion);
    }
}
