﻿namespace Tick.Models.DTO.Habits
{
    public class HabitOutputDTO
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Frequency { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public ICollection<HabitCompletionOutputDTO> HabitCompletions { get; set; } = [];
    }
}
