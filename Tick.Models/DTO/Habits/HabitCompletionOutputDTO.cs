using Tick.Models.Entities;

namespace Tick.Models.DTO.Habits
{
    public class HabitCompletionOutputDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string HabitId { get; set; }
        public DateTime CompletionDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        public required HabitOutputDTO Habit { get; set; }
    }
}
