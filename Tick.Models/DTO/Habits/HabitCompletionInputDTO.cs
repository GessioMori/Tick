namespace Tick.Models.DTO.Habits
{
    public class HabitCompletionInputDTO
    {
        public required string HabitId { get; set; }
        public DateTime CompletionDate { get; set; }
    }
}
