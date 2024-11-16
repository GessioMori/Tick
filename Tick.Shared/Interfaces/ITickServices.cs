using Tick.Models.DTO.Habits;

namespace Tick.Shared.Interfaces
{
    public interface ITickServices
    {
        Task<IEnumerable<HabitOutputDTO>> GetAllHabitsByUserAsync();
        Task<HabitOutputDTO> InsertHabitAsync(HabitInputDTO habit);
    }
}
