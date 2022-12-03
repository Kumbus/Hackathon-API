using Application.Dtos.UserDtos;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;

namespace WebApi.Controllers
{
    [Route("users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly ITokenService _tokenService;
        private readonly IConfiguration _configuration;
        public UsersController(IUsersService usersService, ITokenService tokenService, IConfiguration configuration) 
        {       
            _usersService = usersService;
            _tokenService = tokenService;
            _configuration = configuration;
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

        [HttpPost]
        [Route("googleRegister")]
        public async Task<IActionResult> UserGoogleRegister()
        {
            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> UserLogin([FromBody] UserLoginDto loginDto)
        {
            var user = await _usersService.GetUser(loginDto);

            var token = _tokenService.GenerateJWT(user, _configuration);

            return Ok(new { Token = token });
        }
    }
}
