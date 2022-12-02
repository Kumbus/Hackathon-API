using Application.Dtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        public UsersController(IUsersService usersService) 
        {       
            _usersService = usersService;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> UserRegister(UserRegistrationDto registrationDto)
        {

            var result = await _usersService.AddUser(registrationDto);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);

                return BadRequest(new RegistrationResponseDto { Errors = errors });
            }

            return Ok(new RegistrationResponseDto { IsSuccessfulRegistration = true }); ;
        }

        //[HttpPost]
        //[Route("login")]
        //public async Task<IActionResult> UserLogin(UserLoginDto loginDto)
        //{

        //}
    }
}
