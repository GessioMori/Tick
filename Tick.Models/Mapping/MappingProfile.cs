using AutoMapper;
using Tick.Models.DTO.Habits;
using Tick.Models.Entities;
using Tick.Models.Enums;

namespace Tick.Models.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<HabitInputDTO, Habit>()
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom<HabitFrequencyResolver>());
            CreateMap<Habit, HabitOutputDTO>()
                .ForMember(dest => dest.Frequency, opt => opt.MapFrom<HabitFrequencyResolver>());
        }
    }

    public class HabitFrequencyResolver : IValueResolver<Habit, HabitOutputDTO, string>,
                                           IValueResolver<HabitInputDTO, Habit, HabitFrequency>
    {
        public string Resolve(Habit source, HabitOutputDTO destination, string destMember, ResolutionContext context)
        {
            return source.Frequency.ToString();
        }

        public HabitFrequency Resolve(HabitInputDTO source, Habit destination, HabitFrequency destMember, ResolutionContext context)
        {
            if (Enum.TryParse(typeof(HabitFrequency), source.Frequency, true, out object? frequency))
            {
                return (HabitFrequency)frequency;
            }

            throw new ArgumentException($"Invalid frequency: {source.Frequency}");
        }
    }
}
