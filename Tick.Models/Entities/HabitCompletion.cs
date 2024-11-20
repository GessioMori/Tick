namespace Tick.Models.Entities
{
    public class HabitCompletion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string HabitId { get; set; }
        public required string UserId { get; set; }
        public DateTime CompletionDate { get; set; } = DateTime.UtcNow;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        public required Habit Habit { get; set; }
    }
}
