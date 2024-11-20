using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tick.Models.DTO.Habits;
using Tick.Shared.Consts;
using Tick.Shared.Interfaces;

namespace Tick.API.Controllers.Habits
{
    [Route(Paths.BaseHabitsPath)]
    [ApiController]
    [Authorize]
    public class HabitsController : ControllerBase
    {
        private readonly IHabitsServices _habitService;

        public HabitsController(IHabitsServices habitService)
        {
            this._habitService = habitService;
        }

        [HttpPost()]
        public async Task<IActionResult> InsertHabit([FromBody] HabitInputDTO habitInput)
        {
            if (habitInput == null)
            {
                return BadRequest("Habit input cannot be null.");
            }

            try
            {
                HabitOutputDTO habit = await _habitService.InsertHabitAsync(habitInput);
                return CreatedAtAction(nameof(InsertHabit), new { id = habit.Id }, habit);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { Message = "An error occurred while inserting the habit.", Details = ex.Message });
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<HabitOutputDTO>>> GetAllHabitsByUserAsync()
        {
            try
            {
                IEnumerable<HabitOutputDTO> habits = await _habitService.GetAllHabitsByUserAsync();

                if (habits == null || habits.Any() == false)
                {
                    return NotFound("No habits found for this user.");
                }

                return Ok(habits);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
