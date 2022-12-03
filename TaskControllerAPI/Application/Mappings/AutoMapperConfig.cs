using Application.Dtos.SlotsDtos;
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
                cfg.CreateMap<UserRegistrationDto, User>().ConstructUsing(u => new User(u.FirstName, u.LastName, u.Username, u.Email));
                cfg.CreateMap<User, LoginResponseDto>();
                cfg.CreateMap<PostSlotDto, ActivitySlot>();
                cfg.CreateMap<ActivitySlot, SlotDto>();
                cfg.CreateMap<UpdateSlotDto, ActivitySlot>();
            })
            .CreateMapper();
    }
}
