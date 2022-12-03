using Application.Dtos.SlotsDtos;
using Application.Dtos.TasksDtos;
using Application.Dtos.UserDtos;
using AutoMapper;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserRegistrationDto, User>().ConstructUsing(u => new User(u.Username));
                cfg.CreateMap<User, LoginResponseDto>();
                cfg.CreateMap<PostSlotDto, ActivitySlot>().ConstructUsing(s => new ActivitySlot(s.CategoryOfActivity, s.Name, s.Start, s.QuartersNumber, s.Color));
                cfg.CreateMap<ActivitySlot, SlotDto>();
                cfg.CreateMap<UpdateSlotDto, ActivitySlot>().ConstructUsing(s => new ActivitySlot(s.CategoryOfActivity, s.Name, s.Start, s.QuartersNumber, s.Color));
                cfg.CreateMap<PostTaskDto, PlannedTask>().ConstructUsing(t => new PlannedTask(t.EstimatedMinutes, t.IsCompleted, t.Priority, t.SlotId, t.TaskName));
                cfg.CreateMap<PlannedTask, TaskDto>();
                cfg.CreateMap<UpdateTaskDto, PlannedTask>();
            })
            .CreateMapper();
    }
}
