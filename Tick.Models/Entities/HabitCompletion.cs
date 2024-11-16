namespace Tick.Models.Entities
{
    public class HabitCompletion
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public required string HabitId { get; set; }
        public DateTime CompletionDate { get; set; }

        public required Habit Habit { get; set; }
    }
}
