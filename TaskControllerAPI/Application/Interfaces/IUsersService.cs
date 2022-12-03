using Application.Dtos.UserDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IUsersService
    {
        public Task<IdentityResult> AddUser(UserRegistrationDto registrationDto);
        public Task<LoginResponseDto> GetUser(UserLoginDto loginDto);

        //public Task<
    }
}
