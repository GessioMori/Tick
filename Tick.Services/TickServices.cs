using AutoMapper;
using Tick.Models.DTO.Habits;
using Tick.Models.Entities;
using Tick.Shared.Interfaces;

namespace Tick.Services
{
    public class TickServices : ITickServices
    {
        private readonly ITickDataRepository _tickRepository;
        private readonly IUserContext _userContext;
        private readonly IMapper _mapper;

        public TickServices(ITickDataRepository tickRepository, IUserContext userContext, IMapper mapper)
        {
            this._tickRepository = tickRepository;
            this._userContext = userContext;
            this._mapper = mapper;
        }

        public async Task<IEnumerable<HabitOutputDTO>> GetAllHabitsByUserAsync()
        {
            string userId = GetCurrentUserId();

            IEnumerable<Habit> habits = await this._tickRepository.GetAllHabitsByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<HabitOutputDTO>>(habits);
        }

        public async Task<HabitOutputDTO> InsertHabitAsync(HabitInputDTO habitInput)
        {
            string userId = GetCurrentUserId();

            if (await this._tickRepository.GetHabitByNameAndUserAsync(userId, habitInput.Name))
            {
                throw new Exception("Habit name already registered.");
            }

            Habit habit = this._mapper.Map<Habit>(habitInput);

            habit.UserId = userId;
            habit.CreatedAt = DateTime.UtcNow;

            await this._tickRepository.InsertHabitAsync(habit);

            HabitOutputDTO output = this._mapper.Map<HabitOutputDTO>(habit);

            return output;
        }

        private string GetCurrentUserId()
        {
            string? userId = this._userContext.GetCurrentUserId();

            if (string.IsNullOrEmpty(userId))
            {
                throw new ArgumentNullException(nameof(userId));
            }

            return userId;
        }
    }
}
