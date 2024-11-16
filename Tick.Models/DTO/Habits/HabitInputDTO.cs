using Tick.Models.Enums;

namespace Tick.Models.DTO.Habits
{
    public class HabitInputDTO
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
    }
}
