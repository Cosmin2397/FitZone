using AutoMapper;
using FitZone.ScheduleService.DTOs;
using FitZone.ScheduleService.Entities;
using FitZone.ScheduleService.Entities.Enums;

namespace FitZone.ScheduleService.RequestHelpers
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {


            // Mapare Training -> TrainingDTO (include programările)
            CreateMap<Training, TrainingDTO>()
                .ForMember(dest => dest.ScheduledClients, opt => opt.MapFrom(src => src.ScheduledClients))
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.Type.ToString()))
                .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => src.TrainingStatus.ToString()))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => src.DifficultyLevel.ToString()));


            // Mapare inversă pentru crearea unui Training din DTO
            CreateMap<CreateTrainingDTO, Training>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<TrainingType>(src.Type)))
                .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => Enum.Parse<Status>(src.TrainingStatus)))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => Enum.Parse<DifficultyLevel>(src.DifficultyLevel)));

            // Mapare pentru actualizare Training
            CreateMap<UpdateTrainingDTO, Training>()
                .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => Enum.Parse<Status>(src.TrainingStatus)))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => Enum.Parse<DifficultyLevel>(src.DifficultyLevel)));

            // Mapare TrainingSchedule -> TrainingScheduleDTO
            CreateMap<TrainingSchedule, TrainingScheduleDTO>()
                .ForMember(dest => dest.ScheduleStatus, opt => opt.MapFrom(src => src.ScheduleStatus.ToString()));
            CreateMap<TrainingScheduleDTO, TrainingSchedule>();

            // Mapare inversă pentru creare programare
            CreateMap<CreateTrainingScheduleDTO, TrainingSchedule>();

            CreateMap<TrainingSchedule, UpdateTrainingScheduleDTO>()
                .ForMember(dest => dest.ScheduleStatus, opt => opt.MapFrom(src => src.ScheduleStatus.ToString()));

            CreateMap<UpdateTrainingScheduleDTO, TrainingSchedule>()
                .ForMember(dest => dest.ScheduleStatus, opt => opt.MapFrom(src => Enum.Parse<TrainingScheduleStatus>(src.ScheduleStatus)));

            // Mapare TrainingDTO -> Training (opțional, doar dacă ai nevoie să transformi DTO-ul în entitate)
            CreateMap<TrainingDTO, Training>()
                .ForMember(dest => dest.ScheduledClients, opt => opt.MapFrom(src => src.ScheduledClients)) 
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => Enum.Parse<TrainingType>(src.Type)))
                .ForMember(dest => dest.TrainingStatus, opt => opt.MapFrom(src => Enum.Parse<Status>(src.TrainingStatus)))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => Enum.Parse<DifficultyLevel>(src.DifficultyLevel)));
        }

    }
}
