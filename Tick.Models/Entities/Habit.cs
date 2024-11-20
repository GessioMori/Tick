using Tick.Models.Enums;

namespace Tick.Models.Entities
{
    public class Habit
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string UserId { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public HabitFrequency Frequency { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<HabitCompletion> HabitCompletions { get; set; } = new List<HabitCompletion>();
    }
}
