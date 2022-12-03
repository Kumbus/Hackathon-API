using Application.Dtos.UserDtos;
using Application.Exceptions;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UsersService : IUsersService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public UsersService(IMapper mapper, UserManager<User> userManager)
        {
            _mapper = mapper;
            _userManager = userManager;
        }

        public async Task<IdentityResult> AddUser(UserRegistrationDto registrationDto)
        {
            if (registrationDto == null)
                throw new InvalidCredentialsException();

            var user = _mapper.Map<User>(registrationDto);

            var result = await _userManager.CreateAsync(user, registrationDto.Password);

            return result;
        }

        public async Task<LoginResponseDto> GetUser(UserLoginDto loginDto)
        {
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if(user is null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
                throw new InvalidCredentialsException();

            var response = _mapper.Map<LoginResponseDto>(user);

            return response;


        }
    }
}
